﻿
namespace SnetTestProgram.Forms
{
    partial class FormConnect
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelNetworkIp4 = new System.Windows.Forms.Label();
            this.textBoxNetworkIp4 = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.labelNetworkIp4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNetworkIp4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonConnect, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(848, 80);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelNetworkIp4
            // 
            this.labelNetworkIp4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelNetworkIp4.AutoSize = true;
            this.labelNetworkIp4.Font = new System.Drawing.Font("Bahnschrift SemiLight", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNetworkIp4.Location = new System.Drawing.Point(77, 38);
            this.labelNetworkIp4.Margin = new System.Windows.Forms.Padding(60, 0, 3, 0);
            this.labelNetworkIp4.Name = "labelNetworkIp4";
            this.labelNetworkIp4.Size = new System.Drawing.Size(114, 23);
            this.labelNetworkIp4.TabIndex = 0;
            this.labelNetworkIp4.Text = "Network IP4";
            // 
            // textBoxNetworkIp4
            // 
            this.textBoxNetworkIp4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxNetworkIp4.Location = new System.Drawing.Point(243, 39);
            this.textBoxNetworkIp4.Margin = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.textBoxNetworkIp4.Multiline = true;
            this.textBoxNetworkIp4.Name = "textBoxNetworkIp4";
            this.textBoxNetworkIp4.Size = new System.Drawing.Size(140, 21);
            this.textBoxNetworkIp4.TabIndex = 1;
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnect.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnect.Location = new System.Drawing.Point(454, 30);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(30, 10, 30, 15);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(152, 35);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // FormConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 462);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormConnect";
            this.Text = "FormConnect";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelNetworkIp4;
        private System.Windows.Forms.TextBox textBoxNetworkIp4;
        private System.Windows.Forms.Button buttonConnect;
    }
}