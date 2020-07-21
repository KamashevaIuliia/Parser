using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;



namespace Лаб2КамашеваПарсер
{

    public class Metric
    {

        public static int rowscount = 0;
        public static bool maxrowscount = true;


        public int Id { get; set; }          //        a.Идентификатор угрозы;
        public string Name { get; set; }        //        b.Наименование угрозы;
        public string Description { get; set; }               //        c.Описание угрозы;
        public string Source { get; set; }        //        d.Источник угрозы;
        public string Obyect { get; set; }        //        e.Объект воздействия угрозы;
        public string Konf { get; set; }        //f.Нарушение конфиденциальности(да\нет);
        public string Cel { get; set; }       //        g.Нарушение целостности(да\нет);
        public string Dostup { get; set; }         //        h.Нарушение доступности(да\нет).
        public DateTime On { get; set; }
        public DateTime Change { get; set; }

        public override string ToString()
        {
            return "Идентификатор угрозы" + Id + "/n/rНаименование угрозы" + Name + "/n/rОписание угрозы" + Description + "/n/rИсточник угрозы" + Source + "/n/rОбъект воздействия угрозы" + Obyect + "/n/rНарушение конфиденциальности" + Konf + "/n/rНарушение целостности" + Cel + "/n/rНарушение доступности" + Dostup + "/n/rВремя добавления" + On + "/n/rВремя изменения" + Change ;
        }
    }
    public class ShortMetric
    {
        public string Id { get; set; }          //        a.Идентификатор угрозы;
        public string Name { get; set; }
    }
        public class WorkWithExcel
    {
        
        public static IEnumerable<Metric> EnumerateMetrics(string xlsxpath)
        {

            using (var workbook = new XLWorkbook(xlsxpath))
            {

                var worksheet = workbook.Worksheets.Worksheet(1);
                int length;
                var rows = worksheet.RangeUsed().RowsUsed().Skip(2).ToArray();
                if (rows.Length - (Metric.rowscount * 50 + 3) < 50)
                {
                    length = rows.Length + 3;

                    Metric.maxrowscount = false;
                }
                else
                {
                    length = (Metric.rowscount + 1) * 50 + 3;
                }
                for (int i = Metric.rowscount * 50 + 3; i < length; i++)
                {
                    var metric = new Metric
                    {
                        Id = worksheet.Cell(i, 1).GetValue<int>(),
                        Name = worksheet.Cell(i, 2).GetValue<string>(),
                        Description = worksheet.Cell(i, 3).GetValue<string>(),
                        Source = worksheet.Cell(i, 4).GetValue<string>(),
                        Obyect = worksheet.Cell(i, 5).GetValue<string>(),
                        Konf = worksheet.Cell(i, 6).GetValue<bool>() ? "да" : "нет",
                        Cel = worksheet.Cell(i, 7).GetValue<bool>() ? "да" : "нет",
                        Dostup = worksheet.Cell(i, 8).GetValue<bool>() ? "да" : "нет",
                        On = worksheet.Cell(i, 9).GetValue<DateTime>(),
                        Change = worksheet.Cell(i, 10).GetValue<DateTime>(),

                    };
                    yield return metric;


                }






            }

        }
        public static string Find(string xlsxpath, string choice)
        {
            using (var workbook = new XLWorkbook(xlsxpath))
            {

                var worksheet = workbook.Worksheets.Worksheet(1);

                var rows = worksheet.RangeUsed().RowsUsed().Skip(2);

                foreach (var row in rows)
                {
                    if (choice == Convert.ToString(row.Cell(1).Value))
                    {
                        var metric = new Metric
                        {
                            Id = Convert.ToInt32(row.Cell(1).Value),
                            Name = Convert.ToString(row.Cell(2).Value),
                            Description = Convert.ToString(row.Cell(3).Value),
                            Source = Convert.ToString(row.Cell(4).Value),
                            Obyect = Convert.ToString(row.Cell(5).Value),
                            Konf = Convert.ToBoolean(row.Cell(6).Value) ? "да" : "нет",
                            Cel = Convert.ToBoolean(row.Cell(7).Value) ? "да" : "нет",
                            Dostup = Convert.ToBoolean(row.Cell(8).Value) ? "да" : "нет",
                            On = Convert.ToDateTime(row.Cell(9).Value),
                            Change = Convert.ToDateTime(row.Cell(10).Value),

                        };

                        return "Идентификатор угрозы " + metric.Id + "\n\n\rНаименование угрозы\n\n\r" + metric.Name + "\n\n\rОписание угрозы\n\n\r" + metric.Description + "\n\n\rИсточник угрозы\n\n\r" + metric.Source + "\n\n\rОбъект воздействия угрозы\n\n\r" + metric.Obyect + "\n\n\n\rНарушение конфиденциальности\n\n\r" + metric.Konf + "\n\n\rНарушение целостности\n\n\r" + metric.Cel + "\n\n\rНарушение доступности\n\n\r" + metric.Dostup + "\n\n\rВремя добавления\n\n\r" + metric.On.ToString() + "\n\n\rВремя изменения\n\n\r" + metric.Change.ToString();
                    }
                    
                }
                return "Ooops...";
            }
        }
        public static IEnumerable<ShortMetric> EnumerateMetricsShort(string xlsxpath)
        {

            using (var workbook = new XLWorkbook(xlsxpath))
            {

                var worksheet = workbook.Worksheets.Worksheet(1);
            
                var rows = worksheet.RangeUsed().RowsUsed().Skip(2);
                
                foreach (var row in rows)
                {
                    var metric = new ShortMetric
                    {
                        Id = "УБИ." + Convert.ToString(row.Cell(1).Value),
                        Name = Convert.ToString(row.Cell(2).Value),
                        };


                    yield return metric;
                }






            }

        }
    }

}

