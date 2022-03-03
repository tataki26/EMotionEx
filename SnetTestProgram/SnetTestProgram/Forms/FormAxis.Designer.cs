
namespace SnetTestProgram.Forms
{
    partial class FormAxis
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
            this.dgvAxis = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonConfig = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAxis)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAxis
            // 
            this.dgvAxis.AllowUserToAddRows = false;
            this.dgvAxis.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAxis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAxis.Location = new System.Drawing.Point(12, 12);
            this.dgvAxis.Name = "dgvAxis";
            this.dgvAxis.RowHeadersVisible = false;
            this.dgvAxis.RowTemplate.Height = 23;
            this.dgvAxis.Size = new System.Drawing.Size(252, 123);
            this.dgvAxis.TabIndex = 0;
            // 
            // buttonConfig
            // 
            this.buttonConfig.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConfig.Font = new System.Drawing.Font("Bahnschrift", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConfig.Location = new System.Drawing.Point(164, 146);
            this.buttonConfig.Margin = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.buttonConfig.Name = "buttonConfig";
            this.buttonConfig.Size = new System.Drawing.Size(100, 30);
            this.buttonConfig.TabIndex = 4;
            this.buttonConfig.Text = "Save Config";
            this.buttonConfig.UseVisualStyleBackColor = false;
            this.buttonConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // FormAxis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 186);
            this.Controls.Add(this.buttonConfig);
            this.Controls.Add(this.dgvAxis);
            this.Name = "FormAxis";
            this.Text = "FormAxis";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAxis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAxis;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonConfig;
    }
}