using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CQProgramm
{
    public partial class Form1 : Form
    {
        Dictionary<int, string> Classes = new Dictionary<int, string>();
        Dictionary<string, string> Types = new Dictionary<string, string>();
        Nomenclature[] nomenclatures = new Nomenclature[19];

        public Form1()
        {
            InitializeComponent();
            Classes.Init();
            ProgrammClasscmb.DataSource = new BindingSource(Classes, null);
            ProgrammClasscmb.DisplayMember = "Value";
            ProgrammClasscmb.ValueMember = "Key";
            ProgrammClasscmb.SelectedItem = Classes.Values.First();

            Types.Init();
            Stepcmb.DataSource = new BindingSource(Types, null);
            Stepcmb.DisplayMember = "Value";
            Stepcmb.ValueMember = "Key";
            Stepcmb.SelectedItem = Types.Values.First();

            nomenclatures.Init();

        }

        private void Nextbtn_Click(object sender, EventArgs e)
        {
              
        }
    }
    public static class DictionaryExtension
    {
        public static Dictionary<int, string> Init(this Dictionary<int, string> dict)
        {

            var tmp = dict;
            tmp.Add(5011, "Операционные системы и средства их расширения");
            tmp.Add(5012, "Программные средства управления базами данных");
            tmp.Add(5013, "Инструментально-технические средства программирования");
            tmp.Add(5014, "ПС интерфейса и управления коммуникациями");
            tmp.Add(5015, "ПС организации вычислительного процесса (планирования, контроля)");
            tmp.Add(5016, "Сервисные программы");
            tmp.Add(5017, "ПС обслуживания вычислительной техники");
            tmp.Add(503, "Прикладные программы для научных исследований");
            tmp.Add(504, "Прикладные программы для проектирования");
            tmp.Add(505, "Прикладные программы для управления техническими устройствами и технологическими процессами");

            tmp.Add(506, "Прикладные программы для решения экономических задач");

            tmp.Add(509, "Прочие ПС");


            return tmp;
        }
        public static Dictionary<string, string> Init(this Dictionary<string, string> dict)
        {
            var tmp = dict;
            tmp.Add("analysis", "Анализ");
            tmp.Add("design", "Проектирование");
            tmp.Add("implementation", "Реализация");
            tmp.Add("testing", "Тестирование");
            tmp.Add("release", "Изготовление");
            tmp.Add("support", "Обслуживание (сопровождение)");



            return tmp;
        }
        public static  Nomenclature[] Init(this Nomenclature[] dict)
        {
            var tmp = dict;
            
           
     tmp[0]= new Nomenclature("Устойчивость функционирования",                       new int []  {5011,5012,5013,5014,5015,5016,5017,    504,505,506,509}, new Phase[] { new Phase("Средства восстановления при ошибках на входе", new string[] {"analysis", "design", "implementation", "testing", "release", "support"}, new string[] { "Н0101", "Н0102", "Н0103", "Н0104", "Н0105", "Н0106", "Н0107", "Н0108", "Н0109", "Н0110" }),
                                                                                                                                                                         new Phase ("Средства восстановления при сбоях оборудования",  new string[] {"analysis", "design", "implementation", "testing", "release", "support" }, new string[] {"Н0201","Н0202","Н0203","Н0204","Н0205" }),
                                                                                                                                                                         });

     tmp[1]= new Nomenclature("Работоспособность",                                   new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {});
	 tmp[2]= new Nomenclature("Простота конструкции",                                new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {});
	 tmp[3]= new Nomenclature("Наглядность",                                         new int []  {5011,5012,5013,5014,5015,5016,5017,    504,505,506,509}, new Phase [] {});
	 tmp[4]= new Nomenclature("Структурность",                                       new int []  {5011,5012,5013,5014,5015,5016,5017,    504,505,506,509}, new Phase [] {});
	 tmp[5]= new Nomenclature("Повторяемость",                                       new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {});
	 tmp[6]= new Nomenclature("Легкость освоения",                                   new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {});
	 tmp[7]= new Nomenclature("Доступность эксплутационных программных документов",	 new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {});
	 tmp[8]= new Nomenclature("Удобство эксплуатации",                               new int []  {5011,5012,5013,5014,5015,5016,5017,    504,505,506,509}, new Phase [] {});
	 tmp[9]= new Nomenclature("Критерий уровня автоматизации",                       new int []  {5011,5012,5013,5014,5015,5016,5017,    504,505,506,509}, new Phase [] {});
	 tmp[10]= new Nomenclature("Критерий временной эффективности",                   new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {});
	 tmp[11]= new Nomenclature("Критерий ресурсоемкости",                            new int []  {5011,5012,5013,5014,5015,5016,5017,    504,505,506,509}, new Phase [] {});
	 tmp[12]= new Nomenclature("Гибкость",                                           new int []  {     5012,     5014,5015,              504,505,506,509}, new Phase [] {});
	 tmp[13]= new Nomenclature("Мобильность",                                        new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {});
	 tmp[14]= new Nomenclature("Модифицируемость",                                   new int []  {5011,5012,5013,5014,5015,5016,5017,    504,505,506,509}, new Phase [] {});
	 tmp[15]= new Nomenclature("Полнота реализации",                                 new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {});
	 tmp[16]= new Nomenclature("Согласованность",                                    new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {});
	 tmp[17]= new Nomenclature("Проверенность",                                      new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {});
     tmp[18] = new Nomenclature("Логическая корректность",                           new int[]   {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase[] { });


            return tmp;
        }
    }
}
