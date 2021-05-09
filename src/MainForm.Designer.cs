
namespace GambleGame
{
    partial class MainForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStartBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnStartBtn
            // 
            this.btnStartBtn.Location = new System.Drawing.Point(699, 359);
            this.btnStartBtn.Name = "btnStartBtn";
            this.btnStartBtn.Size = new System.Drawing.Size(75, 23);
            this.btnStartBtn.TabIndex = 0;
            this.btnStartBtn.Text = "Start";
            this.btnStartBtn.UseVisualStyleBackColor = true;
            this.btnStartBtn.Click += new System.EventHandler(this.btnStartBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 576);
            this.Controls.Add(this.btnStartBtn);
            this.Name = "MainForm";
            this.Text = "Roleta do tio Zé";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartBtn;
        private System.Windows.Forms.Timer timer1;
    }
}

