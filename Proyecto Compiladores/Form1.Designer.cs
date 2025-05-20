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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.validar = new System.Windows.Forms.Button();
            this.lexema = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tokensTxt = new System.Windows.Forms.RichTextBox();
            this.NumeroTxt = new System.Windows.Forms.TextBox();
            this.identificadorTxt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.clasificaTokenBtn = new System.Windows.Forms.Button();
            this.dataGridviewTokens = new System.Windows.Forms.DataGridView();
            this.ERRORLINE = new System.Windows.Forms.Label();
            this.ERROR = new System.Windows.Forms.Label();
            this.LR0Boton = new System.Windows.Forms.Button();
            this.dataGridViewLR0 = new System.Windows.Forms.DataGridView();
            this.textBoxEstadosLR0 = new System.Windows.Forms.RichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ClasificaTokens = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.richTextBoxErrores = new System.Windows.Forms.RichTextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.treeViewLR0 = new System.Windows.Forms.TreeView();
            this.Valida = new System.Windows.Forms.Label();
            this.NumAFD = new System.Windows.Forms.Label();
            this.N_Epsi = new System.Windows.Forms.Label();
            this.N_Estados = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridViewIrA = new System.Windows.Forms.DataGridView();
            this.dataGridViewAccion = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridviewTokens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLR0)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIrA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccion)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 52);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 43);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ejecutar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(7, 29);
            this.txtInput.Margin = new System.Windows.Forms.Padding(2);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(376, 20);
            this.txtInput.TabIndex = 1;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(9, 112);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(2);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(374, 20);
            this.txtOutput.TabIndex = 2;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(17, 136);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(454, 494);
            this.dataGridView2.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(82, 52);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 43);
            this.button2.TabIndex = 14;
            this.button2.Text = "Generar AFN";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(638, 62);
            this.label1.TabIndex = 17;
            this.label1.Text = "Plasticentro Lomas Pedregal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(501, 115);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Num de estados:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(398, 115);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Num de Epsilon:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Expresion Regular";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 97);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Expresion Posfija";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(477, 135);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(326, 496);
            this.dataGridView1.TabIndex = 23;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(162, 52);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(70, 43);
            this.button3.TabIndex = 24;
            this.button3.Text = "Generar AFD";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(638, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Num de estados:";
            // 
            // validar
            // 
            this.validar.Location = new System.Drawing.Point(579, 55);
            this.validar.Margin = new System.Windows.Forms.Padding(2);
            this.validar.Name = "validar";
            this.validar.Size = new System.Drawing.Size(74, 20);
            this.validar.TabIndex = 27;
            this.validar.Text = "validar";
            this.validar.UseVisualStyleBackColor = true;
            this.validar.Click += new System.EventHandler(this.validar_Click);
            // 
            // lexema
            // 
            this.lexema.Location = new System.Drawing.Point(454, 54);
            this.lexema.Margin = new System.Windows.Forms.Padding(2);
            this.lexema.Name = "lexema";
            this.lexema.Size = new System.Drawing.Size(121, 20);
            this.lexema.TabIndex = 28;
            this.lexema.TextChanged += new System.EventHandler(this.lexema_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(406, 57);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Lexema";
            // 
            // tokensTxt
            // 
            this.tokensTxt.Location = new System.Drawing.Point(807, 135);
            this.tokensTxt.Margin = new System.Windows.Forms.Padding(2);
            this.tokensTxt.Name = "tokensTxt";
            this.tokensTxt.Size = new System.Drawing.Size(303, 201);
            this.tokensTxt.TabIndex = 31;
            this.tokensTxt.Text = "";
            // 
            // NumeroTxt
            // 
            this.NumeroTxt.Location = new System.Drawing.Point(846, 37);
            this.NumeroTxt.Margin = new System.Windows.Forms.Padding(2);
            this.NumeroTxt.Name = "NumeroTxt";
            this.NumeroTxt.Size = new System.Drawing.Size(66, 20);
            this.NumeroTxt.TabIndex = 35;
            // 
            // identificadorTxt
            // 
            this.identificadorTxt.Location = new System.Drawing.Point(846, 12);
            this.identificadorTxt.Margin = new System.Windows.Forms.Padding(2);
            this.identificadorTxt.Name = "identificadorTxt";
            this.identificadorTxt.Size = new System.Drawing.Size(66, 20);
            this.identificadorTxt.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(789, 39);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Número";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(770, 15);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Identificador";
            // 
            // clasificaTokenBtn
            // 
            this.clasificaTokenBtn.Location = new System.Drawing.Point(651, 7);
            this.clasificaTokenBtn.Margin = new System.Windows.Forms.Padding(2);
            this.clasificaTokenBtn.Name = "clasificaTokenBtn";
            this.clasificaTokenBtn.Size = new System.Drawing.Size(10, 10);
            this.clasificaTokenBtn.TabIndex = 36;
            this.clasificaTokenBtn.Text = "Clasifica Tokens";
            this.clasificaTokenBtn.UseVisualStyleBackColor = true;
            this.clasificaTokenBtn.Click += new System.EventHandler(this.clasificaTokenBtn_Click);
            // 
            // dataGridviewTokens
            // 
            this.dataGridviewTokens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridviewTokens.Location = new System.Drawing.Point(807, 340);
            this.dataGridviewTokens.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridviewTokens.Name = "dataGridviewTokens";
            this.dataGridviewTokens.RowHeadersWidth = 51;
            this.dataGridviewTokens.RowTemplate.Height = 24;
            this.dataGridviewTokens.Size = new System.Drawing.Size(303, 291);
            this.dataGridviewTokens.TabIndex = 37;
            // 
            // ERRORLINE
            // 
            this.ERRORLINE.AutoSize = true;
            this.ERRORLINE.Location = new System.Drawing.Point(789, 89);
            this.ERRORLINE.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ERRORLINE.Name = "ERRORLINE";
            this.ERRORLINE.Size = new System.Drawing.Size(25, 13);
            this.ERRORLINE.TabIndex = 38;
            this.ERRORLINE.Text = "------";
            // 
            // ERROR
            // 
            this.ERROR.AutoSize = true;
            this.ERROR.Location = new System.Drawing.Point(789, 110);
            this.ERROR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ERROR.Name = "ERROR";
            this.ERROR.Size = new System.Drawing.Size(25, 13);
            this.ERROR.TabIndex = 39;
            this.ERROR.Text = "------";
            // 
            // LR0Boton
            // 
            this.LR0Boton.Location = new System.Drawing.Point(597, 7);
            this.LR0Boton.Margin = new System.Windows.Forms.Padding(2);
            this.LR0Boton.Name = "LR0Boton";
            this.LR0Boton.Size = new System.Drawing.Size(129, 19);
            this.LR0Boton.TabIndex = 40;
            this.LR0Boton.Text = "ASA LR(0)";
            this.LR0Boton.UseVisualStyleBackColor = true;
            this.LR0Boton.Click += new System.EventHandler(this.LR0Boton_Click);
            // 
            // dataGridViewLR0
            // 
            this.dataGridViewLR0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLR0.Location = new System.Drawing.Point(18, 30);
            this.dataGridViewLR0.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewLR0.Name = "dataGridViewLR0";
            this.dataGridViewLR0.RowHeadersWidth = 51;
            this.dataGridViewLR0.RowTemplate.Height = 24;
            this.dataGridViewLR0.Size = new System.Drawing.Size(1434, 234);
            this.dataGridViewLR0.TabIndex = 41;
            // 
            // textBoxEstadosLR0
            // 
            this.textBoxEstadosLR0.Location = new System.Drawing.Point(18, 268);
            this.textBoxEstadosLR0.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxEstadosLR0.Name = "textBoxEstadosLR0";
            this.textBoxEstadosLR0.Size = new System.Drawing.Size(275, 396);
            this.textBoxEstadosLR0.TabIndex = 42;
            this.textBoxEstadosLR0.Text = "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(418, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(175, 13);
            this.label10.TabIndex = 45;
            this.label10.Text = "Construir Colección LR(0) Canónica";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 72);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1554, 673);
            this.tabControl1.TabIndex = 46;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ClasificaTokens);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.richTextBoxErrores);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.treeViewLR0);
            this.tabPage1.Controls.Add(this.Valida);
            this.tabPage1.Controls.Add(this.NumAFD);
            this.tabPage1.Controls.Add(this.N_Epsi);
            this.tabPage1.Controls.Add(this.N_Estados);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.txtInput);
            this.tabPage1.Controls.Add(this.txtOutput);
            this.tabPage1.Controls.Add(this.ERROR);
            this.tabPage1.Controls.Add(this.dataGridView2);
            this.tabPage1.Controls.Add(this.ERRORLINE);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.dataGridviewTokens);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.NumeroTxt);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.identificadorTxt);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tokensTxt);
            this.tabPage1.Controls.Add(this.validar);
            this.tabPage1.Controls.Add(this.lexema);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1546, 647);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Analisis Lexico";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ClasificaTokens
            // 
            this.ClasificaTokens.Location = new System.Drawing.Point(828, 63);
            this.ClasificaTokens.Margin = new System.Windows.Forms.Padding(2);
            this.ClasificaTokens.Name = "ClasificaTokens";
            this.ClasificaTokens.Size = new System.Drawing.Size(102, 20);
            this.ClasificaTokens.TabIndex = 47;
            this.ClasificaTokens.Text = "Clasifica Tokens";
            this.ClasificaTokens.UseVisualStyleBackColor = true;
            this.ClasificaTokens.Click += new System.EventHandler(this.ClasificaTokens_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1126, 6);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 13);
            this.label14.TabIndex = 48;
            this.label14.Text = "Errores";
            // 
            // richTextBoxErrores
            // 
            this.richTextBoxErrores.Location = new System.Drawing.Point(1168, 9);
            this.richTextBoxErrores.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBoxErrores.Name = "richTextBoxErrores";
            this.richTextBoxErrores.Size = new System.Drawing.Size(373, 103);
            this.richTextBoxErrores.TabIndex = 47;
            this.richTextBoxErrores.Text = "";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1057, 26);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 79);
            this.button4.TabIndex = 46;
            this.button4.Text = "Analisis Léxico y Sintáctico del programa en lenguaje TINY";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1118, 118);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(180, 13);
            this.label13.TabIndex = 45;
            this.label13.Text = "Árbol de análisis sintáctico resultante";
            // 
            // treeViewLR0
            // 
            this.treeViewLR0.Location = new System.Drawing.Point(1114, 136);
            this.treeViewLR0.Margin = new System.Windows.Forms.Padding(2);
            this.treeViewLR0.Name = "treeViewLR0";
            this.treeViewLR0.Size = new System.Drawing.Size(427, 496);
            this.treeViewLR0.TabIndex = 44;
            // 
            // Valida
            // 
            this.Valida.AutoSize = true;
            this.Valida.Location = new System.Drawing.Point(464, 78);
            this.Valida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Valida.Name = "Valida";
            this.Valida.Size = new System.Drawing.Size(0, 13);
            this.Valida.TabIndex = 43;
            // 
            // NumAFD
            // 
            this.NumAFD.AutoSize = true;
            this.NumAFD.Location = new System.Drawing.Point(729, 115);
            this.NumAFD.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NumAFD.Name = "NumAFD";
            this.NumAFD.Size = new System.Drawing.Size(0, 13);
            this.NumAFD.TabIndex = 42;
            // 
            // N_Epsi
            // 
            this.N_Epsi.AutoSize = true;
            this.N_Epsi.Location = new System.Drawing.Point(483, 115);
            this.N_Epsi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.N_Epsi.Name = "N_Epsi";
            this.N_Epsi.Size = new System.Drawing.Size(0, 13);
            this.N_Epsi.TabIndex = 41;
            // 
            // N_Estados
            // 
            this.N_Estados.AutoSize = true;
            this.N_Estados.Location = new System.Drawing.Point(590, 115);
            this.N_Estados.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.N_Estados.Name = "N_Estados";
            this.N_Estados.Size = new System.Drawing.Size(0, 13);
            this.N_Estados.TabIndex = 40;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.dataGridViewIrA);
            this.tabPage2.Controls.Add(this.dataGridViewAccion);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.dataGridViewLR0);
            this.tabPage2.Controls.Add(this.textBoxEstadosLR0);
            this.tabPage2.Controls.Add(this.LR0Boton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1539, 647);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Analisis Sintactico";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1190, 271);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 49;
            this.label12.Text = "Ir_A";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(584, 271);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 48;
            this.label11.Text = "ACCION";
            // 
            // dataGridViewIrA
            // 
            this.dataGridViewIrA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIrA.Location = new System.Drawing.Point(944, 291);
            this.dataGridViewIrA.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewIrA.Name = "dataGridViewIrA";
            this.dataGridViewIrA.RowHeadersWidth = 51;
            this.dataGridViewIrA.RowTemplate.Height = 24;
            this.dataGridViewIrA.Size = new System.Drawing.Size(520, 372);
            this.dataGridViewIrA.TabIndex = 47;
            // 
            // dataGridViewAccion
            // 
            this.dataGridViewAccion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAccion.Location = new System.Drawing.Point(297, 291);
            this.dataGridViewAccion.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewAccion.Name = "dataGridViewAccion";
            this.dataGridViewAccion.RowHeadersWidth = 51;
            this.dataGridViewAccion.RowTemplate.Height = 24;
            this.dataGridViewAccion.Size = new System.Drawing.Size(643, 372);
            this.dataGridViewAccion.TabIndex = 46;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1562, 748);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clasificaTokenBtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Proyecto Fundamentos de Compiladores";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridviewTokens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLR0)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIrA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button validar;
        private System.Windows.Forms.TextBox lexema;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox tokensTxt;
        private System.Windows.Forms.TextBox NumeroTxt;
        private System.Windows.Forms.TextBox identificadorTxt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button clasificaTokenBtn;
        private System.Windows.Forms.DataGridView dataGridviewTokens;
        private System.Windows.Forms.Label ERRORLINE;
        private System.Windows.Forms.Label ERROR;
        private System.Windows.Forms.Button LR0Boton;
        private System.Windows.Forms.DataGridView dataGridViewLR0;
        private System.Windows.Forms.RichTextBox textBoxEstadosLR0;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label Valida;
        private System.Windows.Forms.Label NumAFD;
        private System.Windows.Forms.Label N_Epsi;
        private System.Windows.Forms.Label N_Estados;
        private System.Windows.Forms.DataGridView dataGridViewIrA;
        private System.Windows.Forms.DataGridView dataGridViewAccion;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TreeView treeViewLR0;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RichTextBox richTextBoxErrores;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button ClasificaTokens;
    }
}

