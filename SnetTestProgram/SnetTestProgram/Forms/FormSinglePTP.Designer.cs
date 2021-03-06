
namespace SnetTestProgram.Forms
{
    partial class FormSinglePTP
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbDwell = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbDecTime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbAccTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbVelocity = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRepeatNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPosition_1 = new System.Windows.Forms.TextBox();
            this.tbPosition_2 = new System.Windows.Forms.TextBox();
            this.tbAxis_1 = new System.Windows.Forms.TextBox();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.tbResult2 = new System.Windows.Forms.TextBox();
            this.tbType = new System.Windows.Forms.TextBox();
            this.timerSPTP = new System.Windows.Forms.Timer(this.components);
            this.timerCount = new System.Windows.Forms.Timer(this.components);
            this.btnRelease = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.10584F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.04708F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.48211F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.10584F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tbDwell, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbDecTime, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbAccTime, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbVelocity, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbRepeatNumber, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbPosition_1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbPosition_2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbAxis_1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbResult, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonStop, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.tbResult2, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbType, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonStart, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnRelease, 4, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(56, 47);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(689, 356);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label4.Size = new System.Drawing.Size(39, 23);
            this.label4.TabIndex = 34;
            this.label4.Text = "Axis";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 300);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label12.Size = new System.Drawing.Size(55, 23);
            this.label12.TabIndex = 31;
            this.label12.Text = "Dwell ";
            // 
            // tbDwell
            // 
            this.tbDwell.Location = new System.Drawing.Point(168, 303);
            this.tbDwell.Name = "tbDwell";
            this.tbDwell.Size = new System.Drawing.Size(100, 21);
            this.tbDwell.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 250);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label9.Size = new System.Drawing.Size(76, 23);
            this.label9.TabIndex = 28;
            this.label9.Text = "Dec Time";
            // 
            // tbDecTime
            // 
            this.tbDecTime.Location = new System.Drawing.Point(168, 253);
            this.tbDecTime.Name = "tbDecTime";
            this.tbDecTime.Size = new System.Drawing.Size(100, 21);
            this.tbDecTime.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 200);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label6.Size = new System.Drawing.Size(74, 23);
            this.label6.TabIndex = 25;
            this.label6.Text = "Acc Time";
            // 
            // tbAccTime
            // 
            this.tbAccTime.Location = new System.Drawing.Point(168, 203);
            this.tbAccTime.Name = "tbAccTime";
            this.tbAccTime.Size = new System.Drawing.Size(100, 21);
            this.tbAccTime.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 150);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label3.Size = new System.Drawing.Size(65, 23);
            this.label3.TabIndex = 22;
            this.label3.Text = "Velocity";
            // 
            // tbVelocity
            // 
            this.tbVelocity.Location = new System.Drawing.Point(168, 153);
            this.tbVelocity.Name = "tbVelocity";
            this.tbVelocity.Size = new System.Drawing.Size(100, 21);
            this.tbVelocity.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 100);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label2.Size = new System.Drawing.Size(123, 23);
            this.label2.TabIndex = 32;
            this.label2.Text = "Repeat Number";
            // 
            // tbRepeatNumber
            // 
            this.tbRepeatNumber.Location = new System.Drawing.Point(168, 103);
            this.tbRepeatNumber.Name = "tbRepeatNumber";
            this.tbRepeatNumber.Size = new System.Drawing.Size(100, 21);
            this.tbRepeatNumber.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 50);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label1.Size = new System.Drawing.Size(70, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Position ";
            // 
            // tbPosition_1
            // 
            this.tbPosition_1.Location = new System.Drawing.Point(168, 53);
            this.tbPosition_1.Name = "tbPosition_1";
            this.tbPosition_1.Size = new System.Drawing.Size(100, 21);
            this.tbPosition_1.TabIndex = 4;
            // 
            // tbPosition_2
            // 
            this.tbPosition_2.Location = new System.Drawing.Point(299, 53);
            this.tbPosition_2.Name = "tbPosition_2";
            this.tbPosition_2.Size = new System.Drawing.Size(100, 21);
            this.tbPosition_2.TabIndex = 5;
            // 
            // tbAxis_1
            // 
            this.tbAxis_1.Location = new System.Drawing.Point(168, 3);
            this.tbAxis_1.Name = "tbAxis_1";
            this.tbAxis_1.Size = new System.Drawing.Size(100, 21);
            this.tbAxis_1.TabIndex = 0;
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(558, 53);
            this.tbResult.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(100, 21);
            this.tbResult.TabIndex = 13;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.Location = new System.Drawing.Point(331, 323);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(90, 30);
            this.buttonStart.TabIndex = 20;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStop.Location = new System.Drawing.Point(596, 323);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(90, 30);
            this.buttonStop.TabIndex = 35;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // tbResult2
            // 
            this.tbResult2.Location = new System.Drawing.Point(558, 103);
            this.tbResult2.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.tbResult2.Name = "tbResult2";
            this.tbResult2.Size = new System.Drawing.Size(100, 21);
            this.tbResult2.TabIndex = 36;
            // 
            // tbType
            // 
            this.tbType.Location = new System.Drawing.Point(558, 3);
            this.tbType.Name = "tbType";
            this.tbType.ReadOnly = true;
            this.tbType.Size = new System.Drawing.Size(100, 21);
            this.tbType.TabIndex = 37;
            // 
            // timerSPTP
            // 
            this.timerSPTP.Tick += new System.EventHandler(this.timerSPTP_Tick);
            // 
            // timerCount
            // 
            this.timerCount.Tick += new System.EventHandler(this.timerCount_Tick);
            // 
            // btnRelease
            // 
            this.btnRelease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRelease.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnRelease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelease.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelease.Location = new System.Drawing.Point(462, 323);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(90, 30);
            this.btnRelease.TabIndex = 39;
            this.btnRelease.Text = "Release";
            this.btnRelease.UseVisualStyleBackColor = false;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // FormSinglePTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormSinglePTP";
            this.Text = "SinglePTP";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbDecTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbAccTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbVelocity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRepeatNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPosition_1;
        private System.Windows.Forms.TextBox tbPosition_2;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbAxis_1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbDwell;
        private System.Windows.Forms.Timer timerSPTP;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Timer timerCount;
        private System.Windows.Forms.TextBox tbResult2;
        private System.Windows.Forms.TextBox tbType;
        private System.Windows.Forms.Button btnRelease;
    }
}