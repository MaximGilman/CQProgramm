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
            foreach(Nomenclature nomen in nomenclatures.Where(x=>(x.Subclasses as IList<int>).Contains(((KeyValuePair<int, string>)ProgrammClasscmb.SelectedItem).Key)))
            {
                foreach (Phase phase in nomen.Phases.Where(x => (x.Types as IList<string>).Contains(((KeyValuePair<string, string>)Stepcmb.SelectedItem).Key)))
                {
                    foreach (string code in phase.Codes)
                    {
//вывод значения
                    }
                }
            }
              
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
                                                                                                                                                                         new Phase("Реализация управления средствами восстановления", new string[] {"design", "implementation", "testing", "release", "support" } , new string[] {"Н0301","Н0302","Н0303","Н0304","Н0305" }) });

     tmp[1]= new Nomenclature("Работоспособность",                                   new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {new Phase("Функционирование в заданных режимах", new string[] { "implementation", "testing", "release", "support" }, new string[] { "Н0401" }),
                                                                                                                                                                         new Phase ("Обеспечение обработки заданного объема информации", new string[] { "implementation", "testing", "release", "support"}, new string[] {"Н0501","Н0502" })});
	 tmp[2]= new Nomenclature("Простота конструкции",                                new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {new Phase("Простота архитектуры проекта ПС", new string[] { "analysis", "design", "support" }, new string[] { "С0101", "С0102" }),
                                                                                                                                                                         new Phase("Сложность архитектуры проекта", new string[]{"design", "implementation", "testing", "release", "support" } , new string[] { "С0201"}),
                                                                                                                                                                         new Phase("Межмодульные связи", new string[]{"design" } , new string[] { "С0301", "С0302", "С0303", "С0304", "С030"}),
                                                                                                                                                                         new Phase("Простота кодирования", new string[]{ "design", "implementation", "testing", "release" } , new string[] {"С1001", "С1002"}),
                                                                                                                                                                                                                                        });
	 tmp[3]= new Nomenclature("Наглядность",                                         new int []  {5011,5012,5013,5014,5015,5016,5017,    504,505,506,509}, new Phase [] {new Phase("Экспертиза принятой системы идентификации", new string[] { "implementation", "testing", "release", "support" }, new string[] { "С0401" }),
                                                                                                                                                                         new Phase ("Комментарии логики программ проекта",new string[]{"implementation", "testing", "release", "support" }, new string[]{"С0801", "С0802", "С0803" }),
                                                                                                                                                                         new Phase ("Оформление текста программы",new string[]{"implementation", "testing", "release", "support" }, new string[]{"С0901", "С0902","С0903" }) });
	 tmp[4]= new Nomenclature("Структурность",                                       new int []  {5011,5012,5013,5014,5015,5016,5017,    504,505,506,509}, new Phase [] {new Phase("Использование основных логических структур", new string[] {"implementation", "testing", "release", "support"}, new string[] {"С0501" }),
                                                                                                                                                                         new Phase("Соблюдение принципа нисходящего программирования", new string[] {"implementation", "testing", "release", "support" }, new string[] {"С0601", "С0602", "С0603", "С0604" }),
                                                                                                                                                                         new Phase("Комментарии обоснования декомпозиции программ при кодировании", new string[] {"implementation", "testing", "release", "support"}, new string[] { "С0701"})});
	 tmp[5]= new Nomenclature("Повторяемость",                                       new int []  {5011,5012,5013,5014,5015,5016,5017,503,504,505,506,509}, new Phase [] {new Phase ("Использование типовых компонентов ПС", new string[] { "analysis", "design" } , new string[]{ "С1301"}),
                                                                                                                                                                         new Phase ("Использование типовых проектных решений", new string[] { "design" } , new string[]{"С1401" })});
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
}//                                                                                                                                                                                 new Phase (, new string[] { } , new string[]{ }
