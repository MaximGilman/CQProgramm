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
        Dictionary<string, string> Elements = new Dictionary<string, string>();
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
            Elements.InitElements();
        }

        private void Nextbtn_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            var yPosition = 0;
             foreach (Nomenclature nomen in nomenclatures.Where(x => (x.Subclasses as IList<int>).Contains(((KeyValuePair<int, string>)ProgrammClasscmb.SelectedItem).Key)))
            {
                foreach (Phase phase in nomen.Phases.Where(x => (x.Types as IList<string>).Contains(((KeyValuePair<string, string>)Stepcmb.SelectedItem).Key)))
                {
                    foreach (string code in phase.Codes)
                    {
                        if (!phase.Contains(code)) { 
                        var label = new Label
                        {
                            Name = string.Format("Label{0}", code),
                            //Tag = string.Format("Label {0}",code),
                            Text = Elements.Where(x => x.Key == code).Select(y => y.Key+": "+y.Value).FirstOrDefault(),
                            Location = new Point(0, yPosition),
                            AutoSize = true
                        };
                        yPosition += 20;

                        Controls.Add(label);
                        var track = new TrackBar
                        {
                            Name = string.Format("TrackBar{0}", code),
                            Maximum = 100,
                            TickFrequency = 100,
                            Location = new Point(0, yPosition),
                            Value = 50
                        };
                        Controls.Add(track);
                        var tracklabel = new Label
                        {
                            Name = string.Format("LabelTrackBar{0}", code),
                            //Tag = string.Format("Label {0}",code),
                             Location = new Point(100, yPosition),
                            AutoSize = true
                        };
                        Controls.Add(tracklabel);

                        track.Scroll += track_Scroll;
                        yPosition += 50;

                            //Button new_label = new Button();
                            //new_label.Name = code;
                            //new_label.Text = Elements.Where(x => x.Key == code).Select(y => y.Value).FirstOrDefault();
                            //this.Controls.Add(new_label);
                            //вывод значения
                        }
                        else
                        {
                            var item = phase.GetElem(code);
                            //заголовок
                            var label = new Label
                            {
                                Name = string.Format("Label{0}", code),
                                //Tag = string.Format("Label {0}",code),
                                Text = item.Text,
                                Location = new Point(0, yPosition),
                                AutoSize = true,
                                Font = new Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold)
                        }; yPosition += 20;
                            Controls.Add(label);


                            //текст левой переменной
                            label = new Label
                            {
                                Name = string.Format("Label{0}", code),
                                //Tag = string.Format("Label {0}",code),
                                Text = item.leftName,
                                Location = new Point(0, yPosition),
                                AutoSize = true
                            }; yPosition += 20;
                            Controls.Add(label);

                            //значение левой переменной
                            var numeric = new NumericUpDown
                            {
                                Name = string.Format("TextBoxL{0}", code),
                                Location = new Point(0, yPosition),

                            }; yPosition += 30;
                            Controls.Add(numeric);
                            


                            //текст правой
                            label = new Label
                            {
                                Name = string.Format("Label{0}", code),
                                //Tag = string.Format("Label {0}",code),
                                Text = item.rightName,
                                Location = new Point(0, yPosition),
                                AutoSize = true
                            }; yPosition += 20;
                            Controls.Add(label);

                            //значение правой
                             numeric = new NumericUpDown
                            {
                                Name = string.Format("TextBoxR{0}", code),
                                Location = new Point(0, yPosition),

                            }; yPosition += 30;
                            Controls.Add(numeric);


                            ////итог
                            //label = new Label
                            //{
                            //    Name = string.Format("AnswerLabel{0}", code),
                            //    Text = item.Calculate().ToString(),
                            //    Location = new Point(0, yPosition),
                            //    AutoSize = true
                            //}; yPosition += 20;
                            //Controls.Add(label);
                        }

                    }

                }

            }
            var Button = new Button
            {
                Name = string.Format("Nextbtn"),
                 Location = new Point(100, yPosition),
                AutoSize = true,
                Text= "Далее"
            };
            Controls.Add(Button);
            Button.Click += Button_Click;

        }
       
        private void Button_Click(object sender, EventArgs e)
        {
            List<char> AlreadyAdded = new List<char>();
 
            var yyPosition = 0;
            foreach (Control c in this.Controls) { c.Visible = false; }
            
            foreach (Nomenclature nomen in nomenclatures.Where(x => (x.Subclasses as IList<int>).Contains(((KeyValuePair<int, string>)ProgrammClasscmb.SelectedItem).Key)))
            {
                var MetricSum = 0.00; var MetricCount = 0;
                var totalCriteria = 0.00; var amountCriteria = 0;
                foreach (Phase phase in nomen.Phases.Where(x => (x.Types as IList<string>).Contains(((KeyValuePair<string, string>)Stepcmb.SelectedItem).Key)))
                {
                    var ElementSum = 0.00; var ElementCount = 0;
                    foreach (string code in phase.Codes)
                    {
                         try
                        {
                            if (phase.Contains(code))
                                ElementSum += Double.IsNaN(phase.Calculate(code, Convert.ToDouble(((NumericUpDown)this.Controls.Find("TextBoxL" + code, true).First()).Value), Convert.ToDouble(((NumericUpDown)this.Controls.Find("TextBoxR" + code, true).First()).Value)))? 0: phase.Calculate(code, Convert.ToDouble(((NumericUpDown)this.Controls.Find("TextBoxL" + code, true).First()).Value), Convert.ToDouble(((NumericUpDown)this.Controls.Find("TextBoxR" + code, true).First()).Value));
                            else
                            {
                                var tracklabel = ((Label)this.Controls.Find("LabelTrackbar" + code, true).First()).Text == "" ? 
                                    "50" : 
                                    ((Label)this.Controls.Find("LabelTrackbar" + code, true).First()).Text.Replace("Текущее значение: ", "");



                                ElementSum += Convert.ToDouble(tracklabel);
                            }
                          

                            ElementCount++;
                         }
                        catch { }
                       
                        
                    }

                    MetricSum += Math.Round(ElementSum / ElementCount,2);
                    MetricCount++;
                }
                {
                     totalCriteria = Math.Round(MetricSum / MetricCount, 2);
                    if (!Double.IsNaN(totalCriteria)) {
                        if (!AlreadyAdded.Contains(nomen.Phases[0].Codes[0].First()))
                        {
                            switch (nomen.Phases[0].Codes[0].First())
                            {
                                case 'Н':
                                    {
                                       var label1 = new Label
                                        {
                                            Name = string.Format("Н"),
                                            Text = "Надежность  ",
                                            Location = new Point(0, yyPosition),
                                            AutoSize = true,
                                           Font = new Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold)

                                       }; yyPosition += 20;
                                        AlreadyAdded.Add(nomen.Phases[0].Codes[0].First());
                                        Controls.Add(label1); break;
                                    }
                                case 'С':
                                    {
                                        var label1 = new Label
                                        {
                                            Name = string.Format("С"),
                                            Text = "Сопровождаемость  ",
                                            Location = new Point(0, yyPosition),
                                            AutoSize = true,
                                            Font = new Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold)

                                        }; yyPosition += 20; AlreadyAdded.Add(nomen.Phases[0].Codes[0].First());

                                        Controls.Add(label1); break;
                                    }
                                case 'У':
                                    {
                                      var  label1 = new Label
                                        {
                                            Name = string.Format("У"),
                                            Text = "Удобство применения  ",
                                            Location = new Point(0, yyPosition),
                                            AutoSize = true,
                                          Font = new Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold)

                                      }; yyPosition += 20; AlreadyAdded.Add(nomen.Phases[0].Codes[0].First());

                                        Controls.Add(label1); break;
                                    }
                                case 'Э':
                                    {
                                        var label1 = new Label
                                        {
                                            Name = string.Format("Э"),
                                            Text = "Эффективность",
                                            Location = new Point(0, yyPosition),
                                            AutoSize = true,
                                            Font = new Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold)

                                        }; yyPosition += 20; AlreadyAdded.Add(nomen.Phases[0].Codes[0].First());

                                        Controls.Add(label1); break;
                                    }
                                case 'Г':
                                    {
                                        var label1 = new Label
                                        {
                                            Name = string.Format("Г"),
                                            Text = "Универсальность",
                                            Location = new Point(0, yyPosition),
                                            AutoSize = true,
                                            Font = new Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold)

                                        }; yyPosition += 20; AlreadyAdded.Add(nomen.Phases[0].Codes[0].First());

                                        Controls.Add(label1); break;
                                    }
                                case 'К':
                                    {
                                        var label1 = new Label
                                        {
                                            Name = string.Format("К"),
                                            Text = "Корректность",
                                            Location = new Point(0, yyPosition),
                                            AutoSize = true,
                                            Font = new Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold)

                                        }; yyPosition += 20; AlreadyAdded.Add(nomen.Phases[0].Codes[0].First());

                                        Controls.Add(label1); break;
                                    }
                            }
                        }


                        var label = new Label
                    {
                        Name = string.Format("Answer{0}",MetricCount),
                        Text = nomen.Title+"  "+ totalCriteria.ToString(),
                        Location = new Point(0, yyPosition),
                        AutoSize = true
                    }; yyPosition += 20;
                    Controls.Add(label);
                 
                    amountCriteria++;
                       
                    }

                }
               
            }
            


        }
            private void track_Scroll(object sender, EventArgs e)
        {
            var name = (sender as TrackBar);
            var tracklabel = (Label)this.Controls.Find("Label"+name.Name, true).First();
            tracklabel.Text = String.Format("Текущее значение: {0}", ((double) name.Value/100.00));
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
        public static Dictionary<string, string> InitElements(this Dictionary<string, string> dict)
        {
            var tmp = dict;
    tmp.Add(    "Н0101", "Наличие требований к программе по устойчивости функционирования при наличии ошибок во входных данных"                                                                   );
    tmp.Add(	"Н0102", "Возможность обработки ошибочных ситуаций"                                                                                                                                );
	tmp.Add(    "Н0103", "Полнота обработки ошибочных ситуаций"                                                                                                                                    );
	tmp.Add(    "Н0104", "Наличие тестов для проверки ошибочных ситуаций"                                                                                                                          );
	tmp.Add(    "Н0105", "Наличие системы контроля полноты входных данных"                                                                                                                         );
	tmp.Add(    "Н0106", "Наличие средств контроля корректности входных данных"                                                                                                                    );
	tmp.Add(    "Н0107", "Наличие средств непротиворечивости входных данных"                                                                                                                       );
	tmp.Add(    "Н0108", "Наличие проверки параметров и адресов по диапазону их значений"                                                                                                          );
	tmp.Add(    "Н0109", "Наличие обработки граничных результатов"                                                                                                                                 );
	tmp.Add(    "Н0110", "Наличиче обработки неопределенностей (деление на 0, квадратный корень из отрицательного числа и т.д.)"                                                                   );
	tmp.Add(    "Н0201", "Наличие требований к прогамме по восстановлению процесса выполнения в случае сбоя операционной системы, процессора, внешних устройств"	                                );
	tmp.Add(    "Н0202", "Наличие требований к программе по восстановлению результатов при отказах процессора, ОС"	                                                                                );
	tmp.Add(    "Н0203", "Наличие средств восстановления процесса в случае сбоев оборудования	"                                                                                                  );
	tmp.Add(    "Н0204", "Наличие возможности разделения по времени выполнения отдельных функций прогамм"	                                                                                        );
	tmp.Add(    "Н0205", "Наличие возможности повторного старта с точки останова"                                                                                                         );
	tmp.Add(    "Н0301", "Наличие централизованного управления процессами, конкурирующими из-за ресурсов" 	                                                                                        );
	tmp.Add(    "Н0302", "Наличие возможности автоматически обходить ошибочные ситуации в процессе вычисления"	                                                                                    );
	tmp.Add(    "Н0303", "Наличие средств, обеспечиающих завершение процесса решения в случае помех"	                                                                                            );
	tmp.Add(    "Н0304", "Наличие средств, обеспечивающих выполнение программы в сокращенном объеме в случае ошибок или помех"	                                                                    );
	tmp.Add(    "Н0305", "Показатель устойчивости к искажающим воздействиям"                                                                                                                       );
	tmp.Add(    "Н0401", "Вероятность безотказной работы работы"                                                                                                                                   );
	tmp.Add(    "Н0501", "Оценка по среднему времени восстановления"                                                                                                                            );
	tmp.Add(    "Н0502", "Оценка по продолжительности преобразования входного набора данных в выходной"                                                                                     );

    tmp.Add(	"С0101", "Наличие модульной схемы программы"                                                                                                                                       );
    tmp.Add(	"С0102", "Оценка программы по числу уникальных модулей");
    tmp.Add(	"С0201", "Наличие ограничений на размеры модуля");
    tmp.Add(	"С0301", "Наличие проверки корректности передаваемых данных");
    tmp.Add(	"С0302", "Оценка простоты программы по числу точек входа и выхода");
    tmp.Add(	"С0303", "Осуществляется ли передача результатов работы модуля через вызывающий его модуль");
    tmp.Add(	"С0304", "Осуществляется ли контроль за правильностью данных, поступающих в вызывающий модуль от вызываемого");
    tmp.Add(	"С030","Наличие требований к независимости модулей программы от типов и форматов выходных данных");
    tmp.Add(	"С1001", "Используется ли язык высокого уровня");
    tmp.Add(	"С1002", "Оценка простоты программы по числу переходов по условию");
	tmp.Add(	"C0401", "Экспертиза принятой системы идентификации");
    tmp.Add(	"С0801", "Наличие комментариев ко всем машинозависимым частям программы");
    tmp.Add(	"С0802", "Наличие комментариев к машинозависимым операторам программы");
    tmp.Add(	"С0803", "Наличие комментариев в точках входа и выхода программы");
    tmp.Add(	"С0901", "Соответствие комментариев принятым соглашениям");
    tmp.Add(	"С0902", "Наличие комментариев- заголовков программы с указанием ее структурных и функциональных характеристик");
    tmp.Add(	"С0903", "Оценка ясности и точности описания последовательности функционирования всех элементов программы");
    tmp.Add(	"С0501", "Использование основных логических структур");
    tmp.Add(	"С0601", "Использование при построении программ метода структурного программирования");
    tmp.Add(	"С0602", "Соблюдение принципа разработки программы сверху вниз");
    tmp.Add(	"С0603", "Оценка программы по числу циклов с одним входом и одним выходом");
    tmp.Add(	"С0604", "Оценка программы по числу циклов");
    tmp.Add(	"С0701", "Комментарии обоснования декомпозиции программ при кодировании");
    tmp.Add(	"С1301", "Использование типовых компонентов ПС");
    tmp.Add(	"С1401", "Использование типовых проектных решений");

    tmp.Add(	"У0101", "Возможность освоения программных средств по документации");
    tmp.Add(	"У0102", "Возможность освоения ПС на контрольном примере при помощи ЭВМ");
    tmp.Add(	"У0103", "Возможность поэтапного освоения ПС");
    tmp.Add(	"У0201", "Полнота и понятность документации для освоения");
    tmp.Add(	"У0202", "Точность документации для освоения");
    tmp.Add(	"У0203", "Техническое исполнение документации");
    tmp.Add(	"У0301", "Наличие краткой аннотации");
    tmp.Add(	"У0302", "Наличие описания решаемых задач");
    tmp.Add(	"У0303", "Наличие описания структуры функций ПС");
    tmp.Add(	"У0304", "Наличие описания основных функций ПС");
    tmp.Add(	"У0306", "Наличие описания частных функций");
    tmp.Add(	"У0307", "Наличие описания алгоритмов");
    tmp.Add(	"У0308", "Наличие описания межмодульных интерфейсов");
    tmp.Add(	"У0309", "Наличие описания пользовательских интерфейсов");
    tmp.Add(	"У0310", "Наличие описания входных и выходных данных"            );
    tmp.Add(	"У0311", "Наличие описания диагностических сообщений"            );
    tmp.Add(	"У0312", "Наличие описания основных характеристик ПС"            );
    tmp.Add(	"У0314", "Наличие описания программной среды функционирования ПС"            );
    tmp.Add(	"У0315", "Достаточность документации для ввода ПС в эксплуатацию"            );
    tmp.Add(	"У0316", "Наличие информации технологии переноса для мобильных программ"            );
    tmp.Add(	"У0401", "Соответствие оглавления содержанию документации"            );
    tmp.Add(	"У0402", "Оценка оформления документации"            );
    tmp.Add(	"У0403", "Грамматическая правильность изложения документации"            );
    tmp.Add(	"У0404", "Отсутствие противоречий"            );
    tmp.Add(	"У0405", "Ясность формулировок и описаний"            );
    tmp.Add(	"У0406", "Ясность формулировок и описаний"            );
    tmp.Add(	"У0407", "Отсутствие неоднозначных формулировок и описаний"            );
    tmp.Add(	"У0408", "Правильность использования терминов"            );
    tmp.Add(	"У0409", "Краткость, отсутствие лишней детализации"            );
    tmp.Add(	"У0410", "Единство формулировок"            );
    tmp.Add(	"У0411", "Единство обозначений"            );
    tmp.Add(	"У0412", "Отсутствие ненужных повторений"            );
    tmp.Add(	"У0413", "Наличие нужных объяснений"            );
    tmp.Add(	"У0501", "Оценка стиля изложения"    );
    tmp.Add(	"У0502", "Дидактическая разделенность"    );
    tmp.Add(	"У0503", "Формальная разделенность"    );
    tmp.Add(	"У0504", "Ясность логической структуры"    );
    tmp.Add(	"У0505", "Соблюдение стандартов и правил изложения в документации"    );
    tmp.Add(	"У0506", "Оценка по числу ссылок вперед в тексте документов"    );
    tmp.Add(	"У0601", "Наличие оглавления"    );
    tmp.Add(	"У0602", "Наличие предметного указателя"    );
    tmp.Add(	"У0603", "Наличие перекрестных ссылок"    );
    tmp.Add(	"У0604", "Наличие всех требуемых разделов"    );
    tmp.Add(	"У0605", "Соблюдение непрерывности нумерации страниц документов"    );
    tmp.Add(	"У0606", "Отсутствие незаконченных разделов абзацев, предложений"    );
    tmp.Add(	"У0607", "Наличие всех рисунков, чертежей, формул, таблиц"    );
    tmp.Add(	"У0608", "Наличие всех строк и примечаний"    );
    tmp.Add(	"У0609", "Логический порядок частей внутри главы"    );
    tmp.Add(	"У0701", "Наличие полного перечня документации"    );
    tmp.Add(	"У0801", "Уровень языка общения пользователя с программой"    );
    tmp.Add(	"У0802", "Легкость и быстрота загрузки и запуска программы"    );
    tmp.Add(	"У0803", "Легкость и быстрота завершения работы программы"    );
    tmp.Add(	"У0804", "Возможность распечатки содержимого программы"    );
    tmp.Add(	"У0805", "Возможность приостанова и повторного запуска работы без потерь информации"    );
    tmp.Add(	"У0901", "Соответствие меню требованиям пользователя"    );
    tmp.Add(	"У0902", "Возможность прямого перехода вверх и вниз по многоуровнему меню (пропуск уровней)"    );
    tmp.Add(	"У1001", "Возможность управления подробностью получаемых выходных данных"    );
    tmp.Add(	"У1002", "Достаточность полученной информации для продолжения работы"    );
    tmp.Add(	"У1101", "Обеспечение удобства ввода данных"    );
    tmp.Add(	"У1102", "Легкость восприятия"    );
    tmp.Add(	"У1201", "Обеспечение программой выполнения предусмотренных рабочих процедур"    );
    tmp.Add(	"У1202", "Достаточность информации, выдаваемой программой для составления дополнительных процедур"    );

            tmp.Add(	"Э0101", "Проблемно-ориентированные функции"    );
    tmp.Add(	"Э0102", "Машинно-ориентированные функции"    );
    tmp.Add(	"Э0103", "Функции ведения и управления"    );
    tmp.Add(	"Э0104", "Функции ввода/вывода"    );
    tmp.Add(	"Э0105", "Функции защиты и проверки данных"    );
    tmp.Add(	"Э0106", "Функции защиты от несанкционированного доступа"    );
    tmp.Add(	"Э0107", "Функции контроля доступа"    );
    tmp.Add(	"Э0108", "Функции защиты от внесения изменений"    );
    tmp.Add(	"Э0109", "Наличие соответствующих границ функциональных областей"    );
    tmp.Add(	"Э0110", "Число знаков после запятой в результатах вычислений"    );
    tmp.Add(	"Э0201", "Время выполнения программ"    );
    tmp.Add(	"Э0202", "Время реакции и ответов"    );
    tmp.Add(	"Э0203", "Время подготовки"    );
    tmp.Add(	"Э0205", "Затраты времени на защиту данных"    );
    tmp.Add(	"Э0206", "Время компиляции"    );
    tmp.Add(	"Э0301", "Требуемый объем внутренней памяти"    );
    tmp.Add(	"Э0302", "Требуемый объем внешней памяти"    );
    tmp.Add(	"Э0303", "Требуемые периферийные устройства"    );
    tmp.Add(	"Э0304", "Требуемое базовое программное обеспечение"    );
    tmp.Add(	"Г0101", "Оценка числа потенциальных пользователей"    );
    tmp.Add(	"Г0102", "Оценка числа функций ПС"    );
    tmp.Add(	"Г0103", "Насколько набор функций удовлетворяет требованиям пользователя"    );
    tmp.Add(	"Г0104", "Насколько возможности программ охватывают область решаемых пользователем задач"    );
    tmp.Add(	"Г0105", "Возможность настройки формата выходных данных для конкретных пользователей"    );
    tmp.Add(	"Г0201", "Наличие схемы иерархии модулей программы"    );
    tmp.Add(	"Г0202", "Оценка независимости модулей"    );
    tmp.Add(	"Г0203", "Оценка числа уникальных элементов/реквизитов"    );
    tmp.Add(	"Г0204", "Используется ли в текущем вызове модуля информация, полученная в предыдущем вызове"    );
    tmp.Add(	"Г0205", "Оценка организации точек входа и выхода модуля"    );
    tmp.Add(	"Г0206", "Наличие описания атрибутов модуля"    );
    tmp.Add(	"Г0301", "Оценка программ по числу переходов и точек ветвления"    );
    tmp.Add(	"Г0401", "Использование метода пошагового уточнения"    );
    tmp.Add(	"Г0402", "Наличие описания структуры программ"    );
    tmp.Add(	"Г0403", "Наличие описания связей между элементами структуры программы"    );
    tmp.Add(	"Г0404", "Наличие в программе повторного выполнения функций (подпрограмм)"    );
    tmp.Add(	"Г0501", "Использование стандартных протоколов связи"    );
    tmp.Add(	"Г0601", "Использование стандартных интерфейсных подпрограмм"    );
    tmp.Add(	"Г0701", "Оценка зависимости программ от емкости оперативной памяти ЭВМ"    );
    tmp.Add(	"Г0702", "Оценка зависимости временных характеристик программы от скорости вычислений ЭВМ"    );
    tmp.Add(	"Г0703", "Оценка зависимости функционирования программы от числа внешних запоминающих устройств и их общей емкости"    );
    tmp.Add(	"Г0704", "Оценка зависимости функционирования программы от специальных устройств ввода-вывода"    );
    tmp.Add(	"Г0801", "Применение специальных языков программирования"    );
    tmp.Add(	"Г0802", "Оценка зависимости программы от программ операционной системы"    );
    tmp.Add(	"Г0803", "Зависимость от других программных средств"    );
    tmp.Add(	"Г0901", "Оценка локализации непереносимой части программы"    );
    tmp.Add(	"Г1001", "Оценка использования отрицательных или булевых выражений"    );
    tmp.Add(	"Г1002", "Оценка программы по использованию условных переходов"    );
    tmp.Add(	"Г1003", "Оценка программы по использованию безусловных переходов"    );
    tmp.Add(	"Г1004", "Оформление процедур входа и выхода из циклов"    );
    tmp.Add(	"Г1005", "Ограничения на модификацию переменной индексации в цикле"    );
    tmp.Add(	"Г1007", "Оценка программы по использованию локальных переменных"    );
    tmp.Add(	"Г1006", "Оценка модулей по направлению потока управления"    );
    tmp.Add(	"Г1101", "Оценка программы по числу комментариев"    );
    tmp.Add(	"Г1201", "Наличие заголовка в программе"    );
    tmp.Add(	"Г1202", "Комментарии к точкам ветвлений"    );
    tmp.Add(	"Г1203", "Комментарии к машинозависимым частям программы"    );
    tmp.Add(	"Г1204", "Комментарии к машинозависимым операторам программы"    );
    tmp.Add(	"Г1205", "Комментарии к операторам объявления переменных"    );
    tmp.Add(	"Г1206", "Оценка семантики операторов"    );
    tmp.Add(	"Г1207", "Наличие соглашений по форме представления комментариев"    );
    tmp.Add(	"Г1208", "Наличие общих комментариев к программам"    );
    tmp.Add(	"Г1301", "Использование языков высокого уровня"    );
    tmp.Add(	"Г1302", "Семантика имен используемых переменных"    );
    tmp.Add(	"Г1303", "Использование отступов, сдвигов и пропусков при формировании текста"    );
    tmp.Add(	"Г1304", "Размещение операторов по строкам"    );
    tmp.Add(	"Г1401", "Передача информации для управления по параметрам"    );
    tmp.Add(	"Г1402", "Параметрическая передача входных данных"    );
    tmp.Add(	"Г1403", "Наличие передачи результатов работы между модулями"    );
    tmp.Add(	"Г1404", "Наличие проверки правильности данных, получаемых модулями от вызываемого модуля"    );
    tmp.Add(	"Г1405", "Использование общих областей памяти"    );

    tmp.Add(	"К0101", "Наличие всех необходимых документов для понимания и использования ПС"    );
    tmp.Add(	"К0102", "Наличие описания и схемы иерархии модулей программы"    );
    tmp.Add(	"К0103", "Наличие описания основных функций"    );
    tmp.Add(	"К0104", "Наличие описания частных функций"    );
    tmp.Add(	"К0105", "Наличие описания данных"    );
    tmp.Add(	"К0106", "Наличие описания алгоритмов"    );
    tmp.Add(	"К0107", "Наличие описания интерфейсов между модулями"    );
    tmp.Add(	"К0108", "Наличие описания интерфейсов с пользователями"    );
    tmp.Add(	"К0109", "Наличие описания используемых числовых методов"    );
    tmp.Add(	"К0110", "Указаны ли все численные методы"    );
    tmp.Add(	"К0111", "Наличие описания всех параметров"    );
    tmp.Add(	"К0112", "Наличие описания методов настройки системы"    );
    tmp.Add(	"К0113", "Наличие описания всех диагностических сообщений"    );
    tmp.Add(	"К0114", "Наличие описания способов проверки работоспособности программы"    );
    tmp.Add(	"К0201", "Реализация всех исходных модулей"    );
    tmp.Add(	"К0202", "Реализация всех основных функций"    );
    tmp.Add(	"К0203", "Реализация всех частных функций"    );
    tmp.Add(	"К0204", "Реализация всех алгоритмов"    );
    tmp.Add(	"К0205", "Реализация всех взаимосвязей в системе"    );
    tmp.Add(	"К0206", "Реализация всех интерфейсов между модулями"    );
    tmp.Add(	"К0207", "Реализация возможности настройки системы"    );
    tmp.Add(	"К0208", "Реализация диагностики всех граничных и аварийных ситуаций"    );
    tmp.Add(	"К0209", "Наличие определения всех данных (переменные, индексы, массивы и проч.)"    );
    tmp.Add(	"К0210", "Наличие интерфейсов с пользователем"    );
    tmp.Add(	"К0301", "Отсутствие противоречий в описании частных функций"    );
    tmp.Add(	"К0302", "Отсутствие противоречий в описании основных функций в разных документах"    );
    tmp.Add(	"К0303", "Отсутствие противоречий в описании алгоритмов"    );
    tmp.Add(	"К0304", "Отсутствие противоречий в описании взаимосвязей в системе"    );
    tmp.Add(	"К0305", "Отсутствие противоречий в описании интерфейсов между модулями"    );
    tmp.Add(	"К0306", "Отсутствие противоречий в описании интерфейсов с пользователем"    );
    tmp.Add(	"К0307", "Отсутствие противоречий в описании настройки системы"    );
    tmp.Add(	"К0309", "Отсутствие противоречий в описании иерархической структуры сообщений"    );
    tmp.Add(	"К0310", "Отсутствие противоречий в описании диагностических сообщений"    );
    tmp.Add(	"К0311", "Отсутствие противоречий в описании данных"    );
    tmp.Add(	"К0401", "Отсутствие противоречий в выполнении основных функций	"    );
    tmp.Add(	"К0402", "Отсутствие противоречий в выполнении частных функций"    );
    tmp.Add(	"К0403", "Отсутствие противоречий в выполнении алгоритмов"    );
    tmp.Add(	"К0404", "Правильность взаимосвязей"    );
    tmp.Add(	"К0405", "Правильность реализации интерфейса между модулями"    );
    tmp.Add(	"К0406", "Правильность реализации интерфейса с пользователем"    );
    tmp.Add(	"К0407", "Отсутствие противоречий в настройке системы"    );
    tmp.Add(	"К0408", "Отсутствие противоречий в диагностике системы	"    );
    tmp.Add(	"К0409", "Отсутствие противоречий в общих переменных"    );
    tmp.Add(	"К0501", "Единообразие способов вызова модулей"    );
    tmp.Add(	"К0502", "Единообразие процедур возврата управления из модулей"    );
    tmp.Add(	"К0503", "Единообразие способов сохранения информации для возврата"    );
    tmp.Add(	"К0504", "Единообразие способов восстановления информации для возврата"    );
    tmp.Add(	"К0505", "Единообразие организации списков передаваемых параметров"    );
    tmp.Add(	"К0601", "Единообразие наименования каждой переменной и константы"    );
    tmp.Add(	"К0602", "Все ли одинаковые константы встречаются во всех программах под одинаковыми именами"    );
    tmp.Add(	"К0603", "Единообразие определения внешних данных во всех программах"    );
    tmp.Add(	"К0604", "Используются ли разные идентификаторы для разных переменных"    );
    tmp.Add(	"К0605", "Все ли общие переменные объявлены как общие переменные"    );
    tmp.Add(	"К0606", "Наличие определений одинаковых атрибутов"    );
    tmp.Add(	"К0701", "Комплектность документации в соответствии со стандартами"    );
    tmp.Add(	"К0702", "Правильное оформление частей документов"    );
    tmp.Add(	"К0703", "Правильное оформление титульных и заглавных листов документов"    );
    tmp.Add(	"К0704", "Наличие в документах всех разделов в соответствии со стандартами"    );
    tmp.Add(	"К0705", "Полнота содержания разделов в соответствии со стандартами"    );
    tmp.Add(	"К0706", "Деление документов на структурные элементы: разделы, подразделы, пункты, подпункты"    );
    tmp.Add(	"К0801", "Соответствие организации и вычислительного процесса эксплуатационной документации"    );
    tmp.Add(	"К0802", "Правильность заданий на выполнение программы, правильность написания управляющих и операторов (отсутствие ошибок)"    );
    tmp.Add(	"К0803", "Отсутствие ошибок в описании действий пользователя"    );
    tmp.Add(	"К0804", "Отсутствие ошибок в описании запуска"    );
    tmp.Add(	"К0805", "Отсутствие ошибок в описании генерации"    );
    tmp.Add(	"К0806", "Отсутствие ошибок в описании настройки"    );
	tmp.Add(	"К0901", "Соответствие ПС документации"    );
    tmp.Add(	"К1001", "Наличие требований к тестированию программ"    );
    tmp.Add(	"К1002", "Достаточность требований к тестированию программ"    );
    tmp.Add(	"К1003", "Отношение числа модулей, отработавших в процессе тестирования и отладки ( Q т м ) к общему числу модулей ( Q о м )"    );
    tmp.Add(	"К1004", "Отношение числа логических блоков, отработавших в процессе тестирования и отладки ( Q т б ), к общему числу логических блоков в программе ( Q о б )"    );
            tmp.Add("К1101", "Реализация всех решений"        );
            tmp.Add("К1201", "Отсутсвие явных ошибок и достаточность реквизитов");



            return tmp;
        }
        public static Nomenclature[] Init(this Nomenclature[] dict)
        {
            var tmp = dict;


            tmp[0] = new Nomenclature("Устойчивость функционирования", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 504, 505, 506, 509 }, new Phase[] { new Phase("Средства восстановления при ошибках на входе", new string[] {"analysis", "design", "implementation", "testing", "release", "support"}, new string[] { "Н0101", "Н0102", "Н0103", "Н0104", "Н0105", "Н0106", "Н0107", "Н0108", "Н0109", "Н0110" }),
                                                                                                                                                                         new Phase ("Средства восстановления при сбоях оборудования",  new string[] {"analysis", "design", "implementation", "testing", "release", "support" }, new string[] {"Н0201","Н0202","Н0203","Н0204","Н0205" }),
                                                                                                                                                                         new Phase("Реализация управления средствами восстановления", new string[] {"design", "implementation", "testing", "release", "support" } , new string[] {"Н0301","Н0302","Н0303","Н0304","Н0305" }) });

            tmp[1] = new Nomenclature("Работоспособность", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 503, 504, 505, 506, 509 }, new Phase[] {new Phase("Функционирование в заданных режимах", new string[] { "implementation", "testing", "release", "support" }, new string[] { "Н0401" }),
                                                                                                                                                                         new Phase ("Обеспечение обработки заданного объема информации", new string[] { "implementation", "testing", "release", "support"}, new string[] {"Н0501","Н0502" })});
            tmp[2] = new Nomenclature("Простота конструкции", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 503, 504, 505, 506, 509 }, new Phase[] {new Phase("Простота архитектуры проекта ПС", new string[] { "analysis", "design", "support" }, new string[] { "С0101", "С0102" }),
                                                                                                                                                                         new Phase("Сложность архитектуры проекта", new string[]{"design", "implementation", "testing", "release", "support" } , new string[] { "С0201"}),
                                                                                                                                                                         new Phase("Межмодульные связи", new string[]{"design" } , new string[] { "С0301", "С0302", "С0303", "С0304", "С030"}),
                                                                                                                                                                         new Phase("Простота кодирования", new string[]{ "design", "implementation", "testing", "release" } , new string[] {"С1001", "С1002"}),
                                                                                                                                                                                                                                        });
            tmp[3] = new Nomenclature("Наглядность", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 504, 505, 506, 509 }, new Phase[] {new Phase("Экспертиза принятой системы идентификации", new string[] { "implementation", "testing", "release", "support" }, new string[] { "С0401" }),
                                                                                                                                                                         new Phase ("Комментарии логики программ проекта",new string[]{"implementation", "testing", "release", "support" }, new string[]{"С0801", "С0802", "С0803" }),
                                                                                                                                                                         new Phase ("Оформление текста программы",new string[]{"implementation", "testing", "release", "support" }, new string[]{"С0901", "С0902","С0903" }) });
            tmp[4] = new Nomenclature("Структурность", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 504, 505, 506, 509 }, new Phase[] {new Phase("Использование основных логических структур", new string[] {"implementation", "testing", "release", "support"}, new string[] {"С0501" }),
                                                                                                                                                                         new Phase("Соблюдение принципа нисходящего программирования", new string[] {"implementation", "testing", "release", "support" }, new string[] {"С0601", "С0602", "С0603", "С0604" }),
                                                                                                                                                                         new Phase("Комментарии обоснования декомпозиции программ при кодировании", new string[] {"implementation", "testing", "release", "support"}, new string[] { "С0701"})});
            tmp[5] = new Nomenclature("Повторяемость", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 503, 504, 505, 506, 509 }, new Phase[] {new Phase ("Использование типовых компонентов ПС", new string[] { "analysis", "design" } , new string[]{ "С1301"}),
                                                                                                                                                                         new Phase ("Использование типовых проектных решений", new string[] { "design" } , new string[]{"С1401" })});
            tmp[6] = new Nomenclature("Легкость освоения", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 503, 504, 505, 506, 509 }, new Phase[] {new Phase("Освоение работы ПС", new string[] { "release", "support"}, new string[] { "У0101", "У0102", "У0103"}),
                                                                                                                                                                         new Phase ("Документация для освоения", new string[] { "release", "support" } , new string[]{"У0201", "У0202", "У0203" }),
                                                                                                                                                                         new Phase ("Полнота пользовательской документации", new string[] { "У0301", "У0302", "У0303", "У0304", "У0306", "У0307", "У0308", "У0309", "У0310", "У0311", "У0312", "У0314", "У0315", "У0316" } , new string[]{ "implementation", "testing", "release", "support"})});
            tmp[7] = new Nomenclature("Доступность эксплутационных программных документов", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 503, 504, 505, 506, 509 }, new Phase[] {new Phase("Точность пользовательской документации", new string[] {"implementation", "testing", "release", "support" }, new string[] { "У0401", "У0402", "У0403", "У0404", "У0405", "У0406", "У0407", "У0408", "У0409", "У0410", "У0411", "У0412", "У0413"}),
                                                                                                                                                                         new Phase ("Понятность пользовательской документации", new string[] { "implementation", "testing", "release", "support" } , new string[]{"У0501", "У0502", "У0503", "У0504", "У0505", "У0506" }),
                                                                                                                                                                         new Phase ("Техническое исполнение пользовательской документации", new string[] { "implementation", "testing", "release", "support" } , new string[]{ "У0601", "У0602", "У0603", "У0604", "У0605", "У0606", "У0607", "У0608", "У0609"}),
                                                                                                                                                                         new Phase ("Прослеживание вариантов пользовательской документации", new string[] { "implementation", "testing", "release", "support" } , new string[]{"У0701" })});
            tmp[8] = new Nomenclature("Удобство эксплуатации", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 504, 505, 506, 509 }, new Phase[] {new Phase("Эксплуатация", new string[] {"analysis", "design", "implementation", "testing", "release", "support" }, new string[] {"У0801", "У0802", "У0803", "У0804", "У0805" }),
                                                                                                                                                                         new Phase ("Управление меню", new string[] { "analysis", "design", "implementation", "testing", "release", "support" } , new string[]{ "У0901", "У0902"})    ,
                                                                                                                                                                         new Phase ("Функция HELP", new string[] { "analysis", "design", "implementation", "testing", "release", "support" } , new string[]{"У1001", "У1002" })    ,
                                                                                                                                                                         new Phase ("Управление данными", new string[] { "analysis", "design", "implementation", "testing", "release", "support" } , new string[]{ "У1101", "У1102"})    ,
                                                                                                                                                                         new Phase ("Рабочие процедуры (JOBS)", new string[] { "analysis", "design", "implementation", "testing", "release", "support" } , new string[]{"У1201", "У1202" })
     });
            tmp[9] = new Nomenclature("Критерий уровня автоматизации", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 504, 505, 506, 509 }, new Phase[] { new Phase("Уровень автоматизации", new string[] { "analysis", "design", "implementation", "testing", "release", "support" }, new string[] { "Э0101", "Э0102", "Э0103", "Э0104", "Э0105", "Э0106", "Э0107", "Э0108", "Э0109", "Э0110" }) });
            tmp[10] = new Nomenclature("Критерий временной эффективности", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 503, 504, 505, 506, 509 }, new Phase[] { new Phase("Временная эффективность", new string[] { "analysis", "implementation", "testing", "release", "support" }, new string[] { "Э0201", "Э0202", "Э0203", "Э0205", "Э0206" }) });
            tmp[11] = new Nomenclature("Критерий ресурсоемкости", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 504, 505, 506, 509 }, new Phase[] { new Phase("Ресурсоемкость", new string[] { "analysis", "design", "implementation", "testing", "release", "support" }, new string[] { "Э0301", "Э0302", "Э0303", "Э0304" }) });
            tmp[12] = new Nomenclature("Гибкость", new int[] { 5012, 5014, 5015, 504, 505, 506, 509 }, new Phase[] { new Phase("Широта охвата функций", new string[] {"analysis", "design", "implementation", "testing", "release", "support" }, new string[] { "Г0101", "Г0102","Г0103", "Г0104", "Г0105"}),
                                                                                                                                                                          new Phase ("Простота архитектуры проекта", new string[] { "design", "implementation", "testing", "release", "support" } , new string[]{"Г0201", "Г0202","Г0203", "Г0204", "Г0205", "Г0206" }),
                                                                                                                                                                          new Phase ("Сложность архитектуры проекта", new string[] { "design", "implementation", "testing", "release", "support" } , new string[]{"Г0301" }),
                                                                                                                                                                          new Phase ("Сложность структуры кода программ", new string[] { "analysis", "design", "implementation", "testing", "release", "support" } , new string[]{"Г0401", "Г0402","Г0403", "Г0404" }),
                                                                                                                                                                          new Phase ("Применение стандартных протоколов связи", new string[] { "implementation", "testing", "release", "support" } , new string[]{ "Г0501"}),
                                                                                                                                                                          new Phase ("Применение стандартных интерфейсных программ", new string[] { "implementation", "testing", "release", "support" } , new string[]{ "Г0601"})});
            tmp[13] = new Nomenclature("Мобильность", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 503, 504, 505, 506, 509 }, new Phase[] { new Phase("Зависимость от используемого комплекса технических средств", new string[] {"analysis", "design", "implementation", "testing", "release", "support" }, new string[] { "Г0701", "Г0702","Г0703", "Г0704"}),
                                                                                                                                                                          new Phase("Зависимость от базового программного обеспечения", new string[] {"analysis", "implementation", "testing", "release", "support" }, new string[] {"Г0801", "Г0802","Г0803" }),
                                                                                                                                                                          new Phase("Изоляция немобильности", new string[] {"analysis", "design", "implementation", "testing", "release", "support" }, new string[] { "Г0901"}) });
            tmp[14] = new Nomenclature("Модифицируемость", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 504, 505, 506, 509 }, new Phase[] { new Phase("Простота кодирования", new string[] { "implementation", "testing", "release", "support"}, new string[] {"Г1001", "Г1002","Г1003", "Г1004", "Г1005", "Г1006", "Г1007" }) ,
                                                                                                                                                                          new Phase ("Число комментариев", new string[] { "implementation", "testing", "release", "support" } , new string[]{ "Г1101"}),
                                                                                                                                                                          new Phase ("Качество комментариев", new string[] { "implementation", "testing", "release", "support" } , new string[]{"Г1201", "Г1202","Г1203", "Г1204", "Г1205", "Г1206", "Г1207", "Г1208" }),
                                                                                                                                                                          new Phase ("Использование описательных средств языка", new string[] { "implementation", "testing", "release", "support" } , new string[]{ "Г1301", "Г1302","Г1303", "Г1304"}),
                                                                                                                                                                          new Phase ("Независимость модулей", new string[] { "implementation", "testing", "release", "support" } , new string[]{"Г1401", "Г1402","Г1403", "Г1404", "Г1405" })});
            tmp[15] = new Nomenclature("Полнота реализации", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 503, 504, 505, 506, 509 }, new Phase[] { new Phase("Полнота документации разработчика", new string[] {"analysis", "design", "implementation", "testing", "release", "support" }, new string[] {"К0101", "К0102","К0103", "К0104", "К0105", "К0106", "К0107", "К0108", "К0109", "К0110", "К0111", "К0112", "К0113", "К0114" }),
                                                                                                                                                                          new Phase ("Полнота программной документации", new string[] { "implementation", "testing", "release", "support" } , new string[]{"К0201", "К0202","К0203", "К0204", "К0205", "К0206", "К0207", "К0208", "К0209", "К0210" })});
            tmp[16] = new Nomenclature("Согласованность", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 503, 504, 505, 506, 509 }, new Phase[] { new Phase("Непротиворечивость документации", new string[] { "design", "implementation", "testing", "release", "support"}, new string[] {"К0301", "К0302","К0303", "К0304", "К0305", "К0306", "К0307", "К0308", "К0309", "К0310", "К0311" }) ,
                                                                                                                                                                          new Phase ("Непротиворечивость программы", new string[] { "implementation", "testing", "release" } , new string[]{"К0401", "К0402","К0403", "К0404", "К0405", "К0406", "К0407", "К0408", "К0409" }),
                                                                                                                                                                          new Phase ("Единообразие интерфейсов между модулями и пользователями", new string[] { "analysis", "design", "implementation", "testing", "release" } , new string[]{"К0501", "К0502","К0503", "К0504", "К0505" }),
                                                                                                                                                                          new Phase ("Единообразие кодирования и определения переменных", new string[] { "analysis", "design", "implementation", "testing", "release", "support" } , new string[]{"К0601", "К0602","К0603", "К0604", "К0605", "К0606" }),
                                                                                                                                                                          new Phase ("Соответствие документации стандартам", new string[] { "design", "implementation", "testing", "release" } , new string[]{ "К0701", "К0702","К0703", "К0704", "К0705", "К0706"}),
                                                                                                                                                                          new Phase ("Соответствие ПС стандартам программирования", new string[] { "analysis", "implementation", "testing", "release", "support" } , new string[]{"К0801", "К0802","К0803", "К0804", "К0805", "К0806" }),
                                                                                                                                                                          new Phase ("Соответствие ПС документации", new string[] { "implementation", "testing", "release", "support" } , new string[]{"К0901" })});
            tmp[17] = new Nomenclature("Проверенность", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 503, 504, 505, 506, 509 }, new Phase[] { new Phase("Полнота тестирования проекта", new string[] { "analysis", "implementation", "testing", "release" }, new string[] { "К1001", "К1002", "К1003", "К1004" }) });
            tmp[18] = new Nomenclature("Логическая корректность", new int[] { 5011, 5012, 5013, 5014, 5015, 5016, 5017, 503, 504, 505, 506, 509 }, new Phase[] { new Phase("Реализация всех решений", new string[] {"analysis", "design", "implementation", "testing", "release", "support" }, new string[] {"К1101" }),
                                                                                                                                                                         new Phase ("Отсутствие явных ошибок и достаточность реквизитов", new string[] { "implementation", "testing", "release", "support" } , new string[]{"К1201"})});


            return tmp;
        }
        public static string[] getTitles(this Countable[] dict)
        {
            string []returner = new string[8];

            for (int i = 0; i < returner.Length; i++)
            {
                returner[i] = dict[i].Title;
            }
            return returner;
        }
    }
}//                                                                                                                                                                                 new Phase (, new string[] { } , new string[]{ })
