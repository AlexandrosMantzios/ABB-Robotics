//namespace Robot_PC_Interface_Panel_Template_New
//{
//    partial class UserControl2
//    {
//        /// <summary> 
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary> 
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Component Designer generated code

//        /// <summary> 
//        /// Required method for Designer support - do not modify 
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.textBox7 = new System.Windows.Forms.TextBox();
//            this.listView2 = new System.Windows.Forms.ListView();
//            this.Signals = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
//            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
//            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
//            this.textBox2 = new System.Windows.Forms.TextBox();
//            this.textBox3 = new System.Windows.Forms.TextBox();
//            this.textBox4 = new System.Windows.Forms.TextBox();
//            this.textBox5 = new System.Windows.Forms.TextBox();
//            this.textBox6 = new System.Windows.Forms.TextBox();
//            this.label9 = new System.Windows.Forms.Label();
//            this.label8 = new System.Windows.Forms.Label();
//            this.label7 = new System.Windows.Forms.Label();
//            this.label6 = new System.Windows.Forms.Label();
//            this.label5 = new System.Windows.Forms.Label();
//            this.label3 = new System.Windows.Forms.Label();
//            this.SuspendLayout();
//            // 
    
//            // 
//            // listView2
//            // 
//            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
//            this.Signals,
//            this.Value,
//            this.columnHeader1});
//            this.listView2.GridLines = true;
//            this.listView2.HideSelection = false;
//            this.listView2.Location = new System.Drawing.Point(349, 17);
//            this.listView2.Name = "listView2";
//            this.listView2.Size = new System.Drawing.Size(411, 282);
//            this.listView2.TabIndex = 31;
//            this.listView2.UseCompatibleStateImageBehavior = false;
//            this.listView2.View = System.Windows.Forms.View.Details;
//            // 
//            // Signals
//            // 
//            this.Signals.Text = "Signals";
//            this.Signals.Width = 186;
//            // 
//            // Value
//            // 
//            this.Value.Text = "Type";
//            this.Value.Width = 110;
//            // 
//            // columnHeader1
//            // 
//            this.columnHeader1.Text = "Value";
//            this.columnHeader1.Width = 112;
//            // 
//            // textBox2
//            // 
//            this.textBox2.Location = new System.Drawing.Point(27, 17);
//            this.textBox2.Name = "textBox2";
//            this.textBox2.Size = new System.Drawing.Size(41, 22);
//            this.textBox2.TabIndex = 32;
//            // 
//            // textBox3
//            // 
//            this.textBox3.Location = new System.Drawing.Point(27, 55);
//            this.textBox3.Name = "textBox3";
//            this.textBox3.Size = new System.Drawing.Size(41, 22);
//            this.textBox3.TabIndex = 33;
//            // 
//            // textBox4
//            // 
//            this.textBox4.Location = new System.Drawing.Point(27, 100);
//            this.textBox4.Name = "textBox4";
//            this.textBox4.Size = new System.Drawing.Size(41, 22);
//            this.textBox4.TabIndex = 34;
//            // 
//            // textBox5
//            // 
//            this.textBox5.Location = new System.Drawing.Point(27, 143);
//            this.textBox5.Name = "textBox5";
//            this.textBox5.Size = new System.Drawing.Size(41, 22);
//            this.textBox5.TabIndex = 35;
//            // 
//            // textBox6
//            // 
//            this.textBox6.Location = new System.Drawing.Point(27, 185);
//            this.textBox6.Name = "textBox6";
//            this.textBox6.Size = new System.Drawing.Size(41, 22);
//            this.textBox6.TabIndex = 36;
//            // 
//            // label9
//            // 
//            this.label9.AutoSize = true;
//            this.label9.Location = new System.Drawing.Point(78, 229);
//            this.label9.Name = "label9";
//            this.label9.Size = new System.Drawing.Size(48, 17);
//            this.label9.TabIndex = 42;
//            this.label9.Text = "Air On";
//            // 
//            // label8
//            // 
//            this.label8.AutoSize = true;
//            this.label8.Location = new System.Drawing.Point(78, 185);
//            this.label8.Name = "label8";
//            this.label8.Size = new System.Drawing.Size(74, 17);
//            this.label8.TabIndex = 41;
//            this.label8.Text = "Motors On";
//            // 
//            // label7
//            // 
//            this.label7.AutoSize = true;
//            this.label7.Location = new System.Drawing.Point(78, 143);
//            this.label7.Name = "label7";
//            this.label7.Size = new System.Drawing.Size(107, 17);
//            this.label7.TabIndex = 40;
//            this.label7.Text = "Gripper_Closed";
//            // 
//            // label6
//            // 
//            this.label6.AutoSize = true;
//            this.label6.Location = new System.Drawing.Point(78, 103);
//            this.label6.Name = "label6";
//            this.label6.Size = new System.Drawing.Size(111, 17);
//            this.label6.TabIndex = 39;
//            this.label6.Text = "Product in Place";
//            // 
//            // label5
//            // 
//            this.label5.AutoSize = true;
//            this.label5.Location = new System.Drawing.Point(78, 58);
//            this.label5.Name = "label5";
//            this.label5.Size = new System.Drawing.Size(84, 17);
//            this.label5.TabIndex = 38;
//            this.label5.Text = "PLC Comms";
//            // 
//            // label3
//            // 
//            this.label3.AutoSize = true;
//            this.label3.Location = new System.Drawing.Point(78, 20);
//            this.label3.Name = "label3";
//            this.label3.Size = new System.Drawing.Size(97, 17);
//            this.label3.TabIndex = 37;
//            this.label3.Text = "Pallet in Place";
//            // 
//            // UserControl2
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.Controls.Add(this.label9);
//            this.Controls.Add(this.label8);
//            this.Controls.Add(this.label7);
//            this.Controls.Add(this.label6);
//            this.Controls.Add(this.label5);
//            this.Controls.Add(this.label3);
//            this.Controls.Add(this.textBox6);
//            this.Controls.Add(this.textBox5);
//            this.Controls.Add(this.textBox4);
//            this.Controls.Add(this.textBox3);
//            this.Controls.Add(this.textBox2);
//            this.Controls.Add(this.listView2);
//            this.Controls.Add(this.textBox7);
//            this.Name = "UserControl2";
//            this.Size = new System.Drawing.Size(829, 333);
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }


//        #endregion

//    private System.Windows.Forms.TextBox textBox7;
//        private System.Windows.Forms.ListView listView2;
//        private System.Windows.Forms.ColumnHeader Signals;
//        private System.Windows.Forms.ColumnHeader Value;
//        private System.Windows.Forms.ColumnHeader columnHeader1;
//        private System.Windows.Forms.TextBox textBox2;
//        private System.Windows.Forms.TextBox textBox3;
//        private System.Windows.Forms.TextBox textBox4;
//        private System.Windows.Forms.TextBox textBox5;
//        private System.Windows.Forms.TextBox textBox6;
//        private System.Windows.Forms.Label label9;
//        private System.Windows.Forms.Label label8;
//        private System.Windows.Forms.Label label7;
//        private System.Windows.Forms.Label label6;
//        private System.Windows.Forms.Label label5;
//        private System.Windows.Forms.Label label3;
//    }
//}
