
namespace NetworkFrame01
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.writeQp = new System.Windows.Forms.Button();
            this.writeLp = new System.Windows.Forms.Button();
            this.readLp = new System.Windows.Forms.Button();
            this.readIp = new System.Windows.Forms.Button();
            this.addr_Qp = new System.Windows.Forms.TextBox();
            this.pointType1 = new System.Windows.Forms.Label();
            this.pointType2 = new System.Windows.Forms.Label();
            this.pointType3 = new System.Windows.Forms.Label();
            this.pointType4 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.addr_Lpw = new System.Windows.Forms.TextBox();
            this.addr_Lpr = new System.Windows.Forms.TextBox();
            this.addr_Ip = new System.Windows.Forms.TextBox();
            this.ip_Result = new System.Windows.Forms.TextBox();
            this.lpr_Result = new System.Windows.Forms.TextBox();
            this.lpw_Result = new System.Windows.Forms.TextBox();
            this.qp_Result = new System.Windows.Forms.TextBox();
            this.data_Ip = new System.Windows.Forms.TextBox();
            this.data_Lpr = new System.Windows.Forms.TextBox();
            this.data_Lpw = new System.Windows.Forms.TextBox();
            this.data_Qp = new System.Windows.Forms.TextBox();
            this.cnntBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // writeQp
            // 
            this.writeQp.Location = new System.Drawing.Point(335, 133);
            this.writeQp.Name = "writeQp";
            this.writeQp.Size = new System.Drawing.Size(80, 21);
            this.writeQp.TabIndex = 0;
            this.writeQp.Text = "Write";
            this.writeQp.UseVisualStyleBackColor = true;
            this.writeQp.Click += new System.EventHandler(this.write_Qp_Click);
            // 
            // writeLp
            // 
            this.writeLp.Location = new System.Drawing.Point(335, 178);
            this.writeLp.Name = "writeLp";
            this.writeLp.Size = new System.Drawing.Size(80, 21);
            this.writeLp.TabIndex = 1;
            this.writeLp.Text = "Write";
            this.writeLp.UseVisualStyleBackColor = true;
            this.writeLp.Click += new System.EventHandler(this.write_Lp_Click);
            // 
            // readLp
            // 
            this.readLp.Location = new System.Drawing.Point(335, 224);
            this.readLp.Name = "readLp";
            this.readLp.Size = new System.Drawing.Size(80, 21);
            this.readLp.TabIndex = 2;
            this.readLp.Text = "Read";
            this.readLp.UseVisualStyleBackColor = true;
            this.readLp.Click += new System.EventHandler(this.read_Lp_Click);
            // 
            // readIp
            // 
            this.readIp.Location = new System.Drawing.Point(335, 274);
            this.readIp.Name = "readIp";
            this.readIp.Size = new System.Drawing.Size(80, 21);
            this.readIp.TabIndex = 3;
            this.readIp.Text = "Read";
            this.readIp.UseVisualStyleBackColor = true;
            this.readIp.Click += new System.EventHandler(this.read_Ip_Click);
            // 
            // addr_Qp
            // 
            this.addr_Qp.Location = new System.Drawing.Point(134, 135);
            this.addr_Qp.Name = "addr_Qp";
            this.addr_Qp.Size = new System.Drawing.Size(39, 21);
            this.addr_Qp.TabIndex = 4;
            // 
            // pointType1
            // 
            this.pointType1.AutoSize = true;
            this.pointType1.Location = new System.Drawing.Point(38, 144);
            this.pointType1.Name = "pointType1";
            this.pointType1.Size = new System.Drawing.Size(38, 12);
            this.pointType1.TabIndex = 8;
            this.pointType1.Text = "Q접점";
            // 
            // pointType2
            // 
            this.pointType2.AutoSize = true;
            this.pointType2.Location = new System.Drawing.Point(38, 187);
            this.pointType2.Name = "pointType2";
            this.pointType2.Size = new System.Drawing.Size(56, 12);
            this.pointType2.TabIndex = 9;
            this.pointType2.Text = "L접점(W)";
            // 
            // pointType3
            // 
            this.pointType3.AutoSize = true;
            this.pointType3.Location = new System.Drawing.Point(38, 235);
            this.pointType3.Name = "pointType3";
            this.pointType3.Size = new System.Drawing.Size(54, 12);
            this.pointType3.TabIndex = 10;
            this.pointType3.Text = "L접점(R)";
            // 
            // pointType4
            // 
            this.pointType4.AutoSize = true;
            this.pointType4.Location = new System.Drawing.Point(38, 285);
            this.pointType4.Name = "pointType4";
            this.pointType4.Size = new System.Drawing.Size(32, 12);
            this.pointType4.TabIndex = 11;
            this.pointType4.Text = "I접점";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label.Location = new System.Drawing.Point(135, 96);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(38, 17);
            this.label.TabIndex = 13;
            this.label.Text = "Addr";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(234, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(581, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Result";
            // 
            // addr_Lpw
            // 
            this.addr_Lpw.Location = new System.Drawing.Point(134, 178);
            this.addr_Lpw.Name = "addr_Lpw";
            this.addr_Lpw.Size = new System.Drawing.Size(39, 21);
            this.addr_Lpw.TabIndex = 16;
            // 
            // addr_Lpr
            // 
            this.addr_Lpr.Location = new System.Drawing.Point(134, 226);
            this.addr_Lpr.Name = "addr_Lpr";
            this.addr_Lpr.Size = new System.Drawing.Size(39, 21);
            this.addr_Lpr.TabIndex = 17;
            // 
            // addr_Ip
            // 
            this.addr_Ip.Location = new System.Drawing.Point(134, 276);
            this.addr_Ip.Name = "addr_Ip";
            this.addr_Ip.Size = new System.Drawing.Size(39, 21);
            this.addr_Ip.TabIndex = 18;
            // 
            // ip_Result
            // 
            this.ip_Result.Location = new System.Drawing.Point(441, 275);
            this.ip_Result.Name = "ip_Result";
            this.ip_Result.Size = new System.Drawing.Size(320, 21);
            this.ip_Result.TabIndex = 27;
            // 
            // lpr_Result
            // 
            this.lpr_Result.Location = new System.Drawing.Point(441, 225);
            this.lpr_Result.Name = "lpr_Result";
            this.lpr_Result.Size = new System.Drawing.Size(320, 21);
            this.lpr_Result.TabIndex = 26;
            // 
            // lpw_Result
            // 
            this.lpw_Result.Location = new System.Drawing.Point(441, 177);
            this.lpw_Result.Name = "lpw_Result";
            this.lpw_Result.Size = new System.Drawing.Size(320, 21);
            this.lpw_Result.TabIndex = 25;
            // 
            // qp_Result
            // 
            this.qp_Result.Location = new System.Drawing.Point(441, 134);
            this.qp_Result.Name = "qp_Result";
            this.qp_Result.Size = new System.Drawing.Size(320, 21);
            this.qp_Result.TabIndex = 24;
            // 
            // data_Ip
            // 
            this.data_Ip.Location = new System.Drawing.Point(201, 275);
            this.data_Ip.Name = "data_Ip";
            this.data_Ip.Size = new System.Drawing.Size(103, 21);
            this.data_Ip.TabIndex = 31;
            // 
            // data_Lpr
            // 
            this.data_Lpr.Location = new System.Drawing.Point(201, 226);
            this.data_Lpr.Name = "data_Lpr";
            this.data_Lpr.Size = new System.Drawing.Size(103, 21);
            this.data_Lpr.TabIndex = 30;
            // 
            // data_Lpw
            // 
            this.data_Lpw.Location = new System.Drawing.Point(201, 177);
            this.data_Lpw.Name = "data_Lpw";
            this.data_Lpw.Size = new System.Drawing.Size(103, 21);
            this.data_Lpw.TabIndex = 29;
            // 
            // data_Qp
            // 
            this.data_Qp.Location = new System.Drawing.Point(201, 134);
            this.data_Qp.Name = "data_Qp";
            this.data_Qp.Size = new System.Drawing.Size(103, 21);
            this.data_Qp.TabIndex = 28;
            // 
            // cnntBtn
            // 
            this.cnntBtn.Location = new System.Drawing.Point(40, 32);
            this.cnntBtn.Name = "cnntBtn";
            this.cnntBtn.Size = new System.Drawing.Size(699, 35);
            this.cnntBtn.TabIndex = 32;
            this.cnntBtn.Text = "Connect";
            this.cnntBtn.UseVisualStyleBackColor = true;
            this.cnntBtn.Click += new System.EventHandler(this.cnnt_Btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 338);
            this.Controls.Add(this.cnntBtn);
            this.Controls.Add(this.data_Ip);
            this.Controls.Add(this.data_Lpr);
            this.Controls.Add(this.data_Lpw);
            this.Controls.Add(this.data_Qp);
            this.Controls.Add(this.ip_Result);
            this.Controls.Add(this.lpr_Result);
            this.Controls.Add(this.lpw_Result);
            this.Controls.Add(this.qp_Result);
            this.Controls.Add(this.addr_Ip);
            this.Controls.Add(this.addr_Lpr);
            this.Controls.Add(this.addr_Lpw);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label);
            this.Controls.Add(this.pointType4);
            this.Controls.Add(this.pointType3);
            this.Controls.Add(this.pointType2);
            this.Controls.Add(this.pointType1);
            this.Controls.Add(this.addr_Qp);
            this.Controls.Add(this.readIp);
            this.Controls.Add(this.readLp);
            this.Controls.Add(this.writeLp);
            this.Controls.Add(this.writeQp);
            this.Name = "Form1";
            this.Text = "Network Frame Maker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button writeQp;
        private System.Windows.Forms.Button writeLp;
        private System.Windows.Forms.Button readLp;
        private System.Windows.Forms.Button readIp;
        private System.Windows.Forms.TextBox addr_Qp;
        private System.Windows.Forms.Label pointType1;
        private System.Windows.Forms.Label pointType2;
        private System.Windows.Forms.Label pointType3;
        private System.Windows.Forms.Label pointType4;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox addr_Lpw;
        private System.Windows.Forms.TextBox addr_Lpr;
        private System.Windows.Forms.TextBox addr_Ip;
        private System.Windows.Forms.TextBox ip_Result;
        private System.Windows.Forms.TextBox lpr_Result;
        private System.Windows.Forms.TextBox lpw_Result;
        private System.Windows.Forms.TextBox qp_Result;
        private System.Windows.Forms.TextBox data_Ip;
        private System.Windows.Forms.TextBox data_Lpr;
        private System.Windows.Forms.TextBox data_Lpw;
        private System.Windows.Forms.TextBox data_Qp;
        private System.Windows.Forms.Button cnntBtn;
    }
}

