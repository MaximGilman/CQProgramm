using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQProgramm
{
    public class Countable : EventArgs
    {
        public string Title;
        public string Text;

        public string leftName;
        public double leftValue;
        public string rightName;
        public double rightValue;
        public Countable(string title, string text, string leftN, double leftV, string rightN, double rightV)
        {
            Title = title;
            Text = text;
            leftName = leftN;
            leftValue = leftV;
            rightName = rightN;
            rightValue = rightV;

        }
        static public Countable[] Init()
        {
            Countable[] countables = new Countable[]
            {
                new Countable("Н0305","Показатель устойчивости к искажающим воздействиям","число экспериментов, в которых искажающие воздействия приводили к отказу",0, "число экспериментов, в которых имитировались искажающие воздействия",0),
               new Countable("Н0401","Вероятность безотказной работы","число зарегистрированных отказов",0, "число экспериментов",0),
               new Countable("Н0501","Оценка по среднему времени восстановления","допустимое среднее время восстановления",0, "среднее время восстановления",0),
               new Countable("Н0502","Оценка по продолжительности преобразования входного набора данных в выходной","допустимое время преобразования i-го входного набора данных",0, "фактическая продолжительность преобразования i-го входного набора данных",0),
                 new Countable("С0302","Оценка простоты программы по числу точек входа и выхода","общее число точек входа в программу",0, "общее число точек выхода из программы",0),
              new Countable("С1002","Оценка простоты программы по числу переходов по условию   ","общее число переходов по условию",0, "общее число исполняемых операторов",0),
                    new Countable("К1003","Отношение числа модулей, отработавших в процессе тестирования и отладки к их общему числу","Число отработанных модулей",0, "Всего модулей",0),

          new Countable("К1004","Отношение числа логических блоков, отработавших в процессе тестирования и отладки к их общему числу","число логических блоков, отработавших в процессе тестирования",0, "Всего блоков",0)


            };
            return countables;

        }
        public double Calculate(double left, double right)
        {
            this.rightValue = right;
            this.leftValue = left;
            switch(this.Title)
            {
                case "Н0305": { return 1 - (leftValue/rightValue); }
                case "Н0401": { return 1 - (leftValue / rightValue); }
                case "Н0501": { if (leftValue >= rightValue) return 1;
                        else return leftValue/rightValue; }
                case "Н0502":
                    {
                        if (leftValue >= rightValue) return 1;
                        else return leftValue / rightValue;
                    }
                case "С0302": { return 1 /((leftValue+1)*(rightValue+1)); }
                case "С1002": { return 1 - (leftValue / rightValue); }
                case "К1003":
                    { return (leftValue / rightValue); }
                case "К1004":
                    { return (leftValue / rightValue); }

            }
            return -1; //error flag
        }

    }
}
