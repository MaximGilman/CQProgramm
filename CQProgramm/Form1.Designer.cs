namespace CQProgramm
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.ТипПС = new System.Windows.Forms.Label();
            this.ProgrammClasscmb = new System.Windows.Forms.ComboBox();
            this.Стадия = new System.Windows.Forms.Label();
            this.Stepcmb = new System.Windows.Forms.ComboBox();
            this.Nextbtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Nextbtn);
            this.panel1.Controls.Add(this.Стадия);
            this.panel1.Controls.Add(this.Stepcmb);
            this.panel1.Controls.Add(this.ТипПС);
            this.panel1.Controls.Add(this.ProgrammClasscmb);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 426);
            this.panel1.TabIndex = 1;
            // 
            // ТипПС
            // 
            this.ТипПС.AutoSize = true;
            this.ТипПС.Location = new System.Drawing.Point(31, 34);
            this.ТипПС.Name = "ТипПС";
            this.ТипПС.Size = new System.Drawing.Size(124, 17);
            this.ТипПС.TabIndex = 1;
            this.ТипПС.Text = "Выберите тип ПС";
            // 
            // ProgrammClasscmb
            // 
            this.ProgrammClasscmb.FormattingEnabled = true;
            this.ProgrammClasscmb.Location = new System.Drawing.Point(14, 57);
            this.ProgrammClasscmb.Name = "ProgrammClasscmb";
            this.ProgrammClasscmb.Size = new System.Drawing.Size(759, 24);
            this.ProgrammClasscmb.TabIndex = 0;
            // 
            // Стадия
            // 
            this.Стадия.AutoSize = true;
            this.Стадия.Location = new System.Drawing.Point(31, 98);
            this.Стадия.Name = "Стадия";
            this.Стадия.Size = new System.Drawing.Size(126, 17);
            this.Стадия.TabIndex = 3;
            this.Стадия.Text = "Выберите стадию";
            // 
            // Stepcmb
            // 
            this.Stepcmb.FormattingEnabled = true;
            this.Stepcmb.Location = new System.Drawing.Point(14, 121);
            this.Stepcmb.Name = "Stepcmb";
            this.Stepcmb.Size = new System.Drawing.Size(759, 24);
            this.Stepcmb.TabIndex = 2;
            // 
            // Nextbtn
            // 
            this.Nextbtn.Location = new System.Drawing.Point(207, 400);
            this.Nextbtn.Name = "Nextbtn";
            this.Nextbtn.Size = new System.Drawing.Size(348, 23);
            this.Nextbtn.TabIndex = 4;
            this.Nextbtn.Text = "Продолжить";
            this.Nextbtn.UseVisualStyleBackColor = true;
            this.Nextbtn.Click += new System.EventHandler(this.Nextbtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox ProgrammClasscmb;
        private System.Windows.Forms.Label ТипПС;
        private System.Windows.Forms.Label Стадия;
        private System.Windows.Forms.ComboBox Stepcmb;
        private System.Windows.Forms.Button Nextbtn;
    }
}

