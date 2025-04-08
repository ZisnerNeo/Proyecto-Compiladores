namespace Proyecto_Compiladores
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.N_Estados = new System.Windows.Forms.Label();
            this.N_Epsi = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.NumAFD = new System.Windows.Forms.Label();
            this.validar = new System.Windows.Forms.Button();
            this.lexema = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Valida = new System.Windows.Forms.Label();
            this.tokensTxt = new System.Windows.Forms.RichTextBox();
            this.NumeroTxt = new System.Windows.Forms.TextBox();
            this.identificadorTxt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.clasificaTokenBtn = new System.Windows.Forms.Button();
            this.dataGridviewTokens = new System.Windows.Forms.DataGridView();
            this.ERRORLINE = new System.Windows.Forms.Label();
            this.ERROR = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridviewTokens)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 143);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 53);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ejecutar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(12, 114);
            this.txtInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(500, 22);
            this.txtInput.TabIndex = 1;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(15, 217);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(497, 22);
            this.txtOutput.TabIndex = 2;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(25, 246);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(606, 447);
            this.dataGridView2.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(113, 143);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 53);
            this.button2.TabIndex = 14;
            this.button2.Text = "Generar AFN";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // N_Estados
            // 
            this.N_Estados.AutoSize = true;
            this.N_Estados.Location = new System.Drawing.Point(775, 226);
            this.N_Estados.Name = "N_Estados";
            this.N_Estados.Size = new System.Drawing.Size(0, 16);
            this.N_Estados.TabIndex = 15;
            // 
            // N_Epsi
            // 
            this.N_Epsi.AutoSize = true;
            this.N_Epsi.Location = new System.Drawing.Point(649, 226);
            this.N_Epsi.Name = "N_Epsi";
            this.N_Epsi.Size = new System.Drawing.Size(0, 16);
            this.N_Epsi.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(794, 78);
            this.label1.TabIndex = 17;
            this.label1.Text = "Plasticentro Lomas Pedregal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(663, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Num de estados:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(541, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "Num de Epsilon:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 16);
            this.label5.TabIndex = 21;
            this.label5.Text = "Expresion Regular";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "Expresion Posfija";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(639, 245);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(433, 447);
            this.dataGridView1.TabIndex = 23;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(219, 143);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 53);
            this.button3.TabIndex = 24;
            this.button3.Text = "Generar AFD";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(861, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = "Num de estados:";
            // 
            // NumAFD
            // 
            this.NumAFD.AutoSize = true;
            this.NumAFD.Location = new System.Drawing.Point(977, 225);
            this.NumAFD.Name = "NumAFD";
            this.NumAFD.Size = new System.Drawing.Size(0, 16);
            this.NumAFD.TabIndex = 26;
            // 
            // validar
            // 
            this.validar.Location = new System.Drawing.Point(775, 146);
            this.validar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.validar.Name = "validar";
            this.validar.Size = new System.Drawing.Size(99, 25);
            this.validar.TabIndex = 27;
            this.validar.Text = "validar";
            this.validar.UseVisualStyleBackColor = true;
            this.validar.Click += new System.EventHandler(this.validar_Click);
            // 
            // lexema
            // 
            this.lexema.Location = new System.Drawing.Point(608, 145);
            this.lexema.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lexema.Name = "lexema";
            this.lexema.Size = new System.Drawing.Size(160, 22);
            this.lexema.TabIndex = 28;
            this.lexema.TextChanged += new System.EventHandler(this.lexema_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(544, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 16);
            this.label7.TabIndex = 29;
            this.label7.Text = "Lexema";
            // 
            // Valida
            // 
            this.Valida.AutoSize = true;
            this.Valida.Location = new System.Drawing.Point(624, 180);
            this.Valida.Name = "Valida";
            this.Valida.Size = new System.Drawing.Size(0, 16);
            this.Valida.TabIndex = 30;
            // 
            // tokensTxt
            // 
            this.tokensTxt.Location = new System.Drawing.Point(1092, 9);
            this.tokensTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tokensTxt.Name = "tokensTxt";
            this.tokensTxt.Size = new System.Drawing.Size(388, 246);
            this.tokensTxt.TabIndex = 31;
            this.tokensTxt.Text = "";
            // 
            // NumeroTxt
            // 
            this.NumeroTxt.Location = new System.Drawing.Point(1587, 188);
            this.NumeroTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NumeroTxt.Name = "NumeroTxt";
            this.NumeroTxt.Size = new System.Drawing.Size(44, 22);
            this.NumeroTxt.TabIndex = 35;
            // 
            // identificadorTxt
            // 
            this.identificadorTxt.Location = new System.Drawing.Point(1587, 158);
            this.identificadorTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.identificadorTxt.Name = "identificadorTxt";
            this.identificadorTxt.Size = new System.Drawing.Size(44, 22);
            this.identificadorTxt.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1511, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 16);
            this.label8.TabIndex = 34;
            this.label8.Text = "Número";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1486, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 16);
            this.label9.TabIndex = 33;
            this.label9.Text = "Identificador";
            // 
            // clasificaTokenBtn
            // 
            this.clasificaTokenBtn.Location = new System.Drawing.Point(1495, 230);
            this.clasificaTokenBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clasificaTokenBtn.Name = "clasificaTokenBtn";
            this.clasificaTokenBtn.Size = new System.Drawing.Size(136, 25);
            this.clasificaTokenBtn.TabIndex = 36;
            this.clasificaTokenBtn.Text = "Clasifica Tokens";
            this.clasificaTokenBtn.UseVisualStyleBackColor = true;
            this.clasificaTokenBtn.Click += new System.EventHandler(this.clasificaTokenBtn_Click);
            // 
            // dataGridviewTokens
            // 
            this.dataGridviewTokens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridviewTokens.Location = new System.Drawing.Point(1092, 270);
            this.dataGridviewTokens.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridviewTokens.Name = "dataGridviewTokens";
            this.dataGridviewTokens.RowHeadersWidth = 51;
            this.dataGridviewTokens.RowTemplate.Height = 24;
            this.dataGridviewTokens.Size = new System.Drawing.Size(388, 404);
            this.dataGridviewTokens.TabIndex = 37;
            // 
            // ERRORLINE
            // 
            this.ERRORLINE.AutoSize = true;
            this.ERRORLINE.Location = new System.Drawing.Point(1486, 282);
            this.ERRORLINE.Name = "ERRORLINE";
            this.ERRORLINE.Size = new System.Drawing.Size(31, 16);
            this.ERRORLINE.TabIndex = 38;
            this.ERRORLINE.Text = "------";
            // 
            // ERROR
            // 
            this.ERROR.AutoSize = true;
            this.ERROR.Location = new System.Drawing.Point(1486, 309);
            this.ERROR.Name = "ERROR";
            this.ERROR.Size = new System.Drawing.Size(31, 16);
            this.ERROR.TabIndex = 39;
            this.ERROR.Text = "------";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2030, 720);
            this.Controls.Add(this.ERROR);
            this.Controls.Add(this.ERRORLINE);
            this.Controls.Add(this.dataGridviewTokens);
            this.Controls.Add(this.clasificaTokenBtn);
            this.Controls.Add(this.NumeroTxt);
            this.Controls.Add(this.identificadorTxt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tokensTxt);
            this.Controls.Add(this.Valida);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lexema);
            this.Controls.Add(this.validar);
            this.Controls.Add(this.NumAFD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.N_Epsi);
            this.Controls.Add(this.N_Estados);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridviewTokens)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label N_Estados;
        private System.Windows.Forms.Label N_Epsi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label NumAFD;
        private System.Windows.Forms.Button validar;
        private System.Windows.Forms.TextBox lexema;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label Valida;
        private System.Windows.Forms.RichTextBox tokensTxt;
        private System.Windows.Forms.TextBox NumeroTxt;
        private System.Windows.Forms.TextBox identificadorTxt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button clasificaTokenBtn;
        private System.Windows.Forms.DataGridView dataGridviewTokens;
        private System.Windows.Forms.Label ERRORLINE;
        private System.Windows.Forms.Label ERROR;
    }
}

