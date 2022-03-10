
namespace SnetTestProgram.Forms
{
    partial class FormInterrupt
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
            this.btnMoveSingle = new System.Windows.Forms.Button();
            this.btnViaPos = new System.Windows.Forms.Button();
            this.btnAngle = new System.Windows.Forms.Button();
            this.btnRadius = new System.Windows.Forms.Button();
            this.btnMultiLine = new System.Windows.Forms.Button();
            this.btnSingleLine = new System.Windows.Forms.Button();
            this.btnMoveMulti = new System.Windows.Forms.Button();
            this.labelPTP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPolling = new System.Windows.Forms.CheckBox();
            this.cbInterrupt = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnMoveSingle
            // 
            this.btnMoveSingle.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnMoveSingle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveSingle.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveSingle.Location = new System.Drawing.Point(126, 234);
            this.btnMoveSingle.Margin = new System.Windows.Forms.Padding(30, 10, 30, 15);
            this.btnMoveSingle.Name = "btnMoveSingle";
            this.btnMoveSingle.Size = new System.Drawing.Size(152, 35);
            this.btnMoveSingle.TabIndex = 3;
            this.btnMoveSingle.Text = "Single";
            this.btnMoveSingle.UseVisualStyleBackColor = false;
            this.btnMoveSingle.Click += new System.EventHandler(this.btnMoveSingle_Click);
            // 
            // btnViaPos
            // 
            this.btnViaPos.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnViaPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViaPos.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViaPos.Location = new System.Drawing.Point(568, 325);
            this.btnViaPos.Margin = new System.Windows.Forms.Padding(30, 10, 30, 15);
            this.btnViaPos.Name = "btnViaPos";
            this.btnViaPos.Size = new System.Drawing.Size(152, 35);
            this.btnViaPos.TabIndex = 4;
            this.btnViaPos.Text = "ViaPos + EndPos";
            this.btnViaPos.UseVisualStyleBackColor = false;
            this.btnViaPos.Click += new System.EventHandler(this.btnViaPos_Click);
            // 
            // btnAngle
            // 
            this.btnAngle.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAngle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAngle.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAngle.Location = new System.Drawing.Point(568, 280);
            this.btnAngle.Margin = new System.Windows.Forms.Padding(30, 10, 30, 15);
            this.btnAngle.Name = "btnAngle";
            this.btnAngle.Size = new System.Drawing.Size(152, 35);
            this.btnAngle.TabIndex = 5;
            this.btnAngle.Text = "MidPos + Angle";
            this.btnAngle.UseVisualStyleBackColor = false;
            this.btnAngle.Click += new System.EventHandler(this.btnAngle_Click);
            // 
            // btnRadius
            // 
            this.btnRadius.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnRadius.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRadius.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRadius.Location = new System.Drawing.Point(568, 234);
            this.btnRadius.Margin = new System.Windows.Forms.Padding(30, 10, 30, 15);
            this.btnRadius.Name = "btnRadius";
            this.btnRadius.Size = new System.Drawing.Size(152, 35);
            this.btnRadius.TabIndex = 6;
            this.btnRadius.Text = "EndPos + Radius";
            this.btnRadius.UseVisualStyleBackColor = false;
            this.btnRadius.Click += new System.EventHandler(this.btnRadius_Click);
            // 
            // btnMultiLine
            // 
            this.btnMultiLine.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnMultiLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMultiLine.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMultiLine.Location = new System.Drawing.Point(343, 325);
            this.btnMultiLine.Margin = new System.Windows.Forms.Padding(30, 10, 30, 15);
            this.btnMultiLine.Name = "btnMultiLine";
            this.btnMultiLine.Size = new System.Drawing.Size(152, 35);
            this.btnMultiLine.TabIndex = 7;
            this.btnMultiLine.Text = "Multi";
            this.btnMultiLine.UseVisualStyleBackColor = false;
            this.btnMultiLine.Click += new System.EventHandler(this.btnMultiLine_Click);
            // 
            // btnSingleLine
            // 
            this.btnSingleLine.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSingleLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSingleLine.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSingleLine.Location = new System.Drawing.Point(343, 234);
            this.btnSingleLine.Margin = new System.Windows.Forms.Padding(30, 10, 30, 15);
            this.btnSingleLine.Name = "btnSingleLine";
            this.btnSingleLine.Size = new System.Drawing.Size(152, 35);
            this.btnSingleLine.TabIndex = 8;
            this.btnSingleLine.Text = "Single";
            this.btnSingleLine.UseVisualStyleBackColor = false;
            this.btnSingleLine.Click += new System.EventHandler(this.btnSingleLine_Click);
            // 
            // btnMoveMulti
            // 
            this.btnMoveMulti.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnMoveMulti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveMulti.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveMulti.Location = new System.Drawing.Point(126, 325);
            this.btnMoveMulti.Margin = new System.Windows.Forms.Padding(30, 10, 30, 15);
            this.btnMoveMulti.Name = "btnMoveMulti";
            this.btnMoveMulti.Size = new System.Drawing.Size(152, 35);
            this.btnMoveMulti.TabIndex = 9;
            this.btnMoveMulti.Text = "Multi";
            this.btnMoveMulti.UseVisualStyleBackColor = false;
            this.btnMoveMulti.Click += new System.EventHandler(this.btnMoveMulti_Click);
            // 
            // labelPTP
            // 
            this.labelPTP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelPTP.AutoSize = true;
            this.labelPTP.Font = new System.Drawing.Font("Bahnschrift SemiLight", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPTP.Location = new System.Drawing.Point(110, 147);
            this.labelPTP.Margin = new System.Windows.Forms.Padding(60, 0, 3, 0);
            this.labelPTP.Name = "labelPTP";
            this.labelPTP.Size = new System.Drawing.Size(122, 23);
            this.labelPTP.TabIndex = 10;
            this.labelPTP.Text = "Point to Point";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift SemiLight", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(309, 147);
            this.label1.Margin = new System.Windows.Forms.Padding(60, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "Line Interpolation";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift SemiLight", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(538, 147);
            this.label2.Margin = new System.Windows.Forms.Padding(60, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 23);
            this.label2.TabIndex = 12;
            this.label2.Text = "Arc Interpolation";
            // 
            // cbPolling
            // 
            this.cbPolling.AutoSize = true;
            this.cbPolling.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPolling.Location = new System.Drawing.Point(343, 63);
            this.cbPolling.Name = "cbPolling";
            this.cbPolling.Size = new System.Drawing.Size(78, 23);
            this.cbPolling.TabIndex = 13;
            this.cbPolling.Text = "Polling";
            this.cbPolling.UseVisualStyleBackColor = true;
            this.cbPolling.CheckedChanged += new System.EventHandler(this.cbPolling_CheckedChanged);
            // 
            // cbInterrupt
            // 
            this.cbInterrupt.AutoSize = true;
            this.cbInterrupt.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbInterrupt.Location = new System.Drawing.Point(500, 63);
            this.cbInterrupt.Name = "cbInterrupt";
            this.cbInterrupt.Size = new System.Drawing.Size(92, 23);
            this.cbInterrupt.TabIndex = 14;
            this.cbInterrupt.Text = "Interrupt";
            this.cbInterrupt.UseVisualStyleBackColor = true;
            this.cbInterrupt.CheckedChanged += new System.EventHandler(this.cbInterrupt_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift SemiLight", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(110, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(60, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 23);
            this.label3.TabIndex = 15;
            this.label3.Text = "Type";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(353, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(92, 16);
            this.radioButton1.TabIndex = 16;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(516, 21);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(92, 16);
            this.radioButton2.TabIndex = 17;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // FormInterrupt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbInterrupt);
            this.Controls.Add(this.cbPolling);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelPTP);
            this.Controls.Add(this.btnMoveMulti);
            this.Controls.Add(this.btnSingleLine);
            this.Controls.Add(this.btnMultiLine);
            this.Controls.Add(this.btnRadius);
            this.Controls.Add(this.btnAngle);
            this.Controls.Add(this.btnViaPos);
            this.Controls.Add(this.btnMoveSingle);
            this.Name = "FormInterrupt";
            this.Text = "Interrupt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMoveSingle;
        private System.Windows.Forms.Button btnViaPos;
        private System.Windows.Forms.Button btnAngle;
        private System.Windows.Forms.Button btnRadius;
        private System.Windows.Forms.Button btnMultiLine;
        private System.Windows.Forms.Button btnSingleLine;
        private System.Windows.Forms.Button btnMoveMulti;
        private System.Windows.Forms.Label labelPTP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbPolling;
        private System.Windows.Forms.CheckBox cbInterrupt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}