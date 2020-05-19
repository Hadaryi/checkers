namespace Checkers
{
    public partial class FormSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonSize6 = new System.Windows.Forms.RadioButton();
            this.radioButtonSize8 = new System.Windows.Forms.RadioButton();
            this.radioButtonSize10 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPlayerOne = new System.Windows.Forms.TextBox();
            this.checkBoxPlayerTwo = new System.Windows.Forms.CheckBox();
            this.textBoxPlayerTwo = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Board Size:";
            // 
            // radioButtonSize6
            // 
            this.radioButtonSize6.AutoSize = true;
            this.radioButtonSize6.Checked = true;
            this.radioButtonSize6.Location = new System.Drawing.Point(25, 29);
            this.radioButtonSize6.Name = "radioButtonSize6";
            this.radioButtonSize6.Size = new System.Drawing.Size(59, 21);
            this.radioButtonSize6.TabIndex = 1;
            this.radioButtonSize6.TabStop = true;
            this.radioButtonSize6.Text = "6 x 6";
            this.radioButtonSize6.UseVisualStyleBackColor = true;
            // 
            // radioButtonSize8
            // 
            this.radioButtonSize8.AutoSize = true;
            this.radioButtonSize8.Location = new System.Drawing.Point(90, 29);
            this.radioButtonSize8.Name = "radioButtonSize8";
            this.radioButtonSize8.Size = new System.Drawing.Size(59, 21);
            this.radioButtonSize8.TabIndex = 2;
            this.radioButtonSize8.TabStop = true;
            this.radioButtonSize8.Text = "8 x 8";
            this.radioButtonSize8.UseVisualStyleBackColor = true;
            // 
            // radioButtonSize10
            // 
            this.radioButtonSize10.AutoSize = true;
            this.radioButtonSize10.Location = new System.Drawing.Point(155, 29);
            this.radioButtonSize10.Name = "radioButtonSize10";
            this.radioButtonSize10.Size = new System.Drawing.Size(75, 21);
            this.radioButtonSize10.TabIndex = 3;
            this.radioButtonSize10.TabStop = true;
            this.radioButtonSize10.Text = "10 x 10";
            this.radioButtonSize10.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Players:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Player 1:";
            // 
            // textBoxPlayerOne
            // 
            this.textBoxPlayerOne.Location = new System.Drawing.Point(112, 76);
            this.textBoxPlayerOne.MaxLength = 20;
            this.textBoxPlayerOne.Name = "textBoxPlayerOne";
            this.textBoxPlayerOne.Size = new System.Drawing.Size(118, 22);
            this.textBoxPlayerOne.TabIndex = 6;
            // 
            // checkBoxPlayerTwo
            // 
            this.checkBoxPlayerTwo.AutoSize = true;
            this.checkBoxPlayerTwo.Location = new System.Drawing.Point(25, 113);
            this.checkBoxPlayerTwo.Name = "checkBoxPlayerTwo";
            this.checkBoxPlayerTwo.Size = new System.Drawing.Size(86, 21);
            this.checkBoxPlayerTwo.TabIndex = 7;
            this.checkBoxPlayerTwo.Text = "Player 2:";
            this.checkBoxPlayerTwo.UseVisualStyleBackColor = true;
            this.checkBoxPlayerTwo.CheckedChanged += new System.EventHandler(this.checkBoxPlayerTwo_CheckedChanged);
            // 
            // textBoxPlayerTwo
            // 
            this.textBoxPlayerTwo.Enabled = false;
            this.textBoxPlayerTwo.Location = new System.Drawing.Point(112, 113);
            this.textBoxPlayerTwo.MaxLength = 20;
            this.textBoxPlayerTwo.Name = "textBoxPlayerTwo";
            this.textBoxPlayerTwo.Size = new System.Drawing.Size(118, 22);
            this.textBoxPlayerTwo.TabIndex = 8;
            this.textBoxPlayerTwo.Text = "[Computer]";
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(153, 150);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 9;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 185);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayerTwo);
            this.Controls.Add(this.checkBoxPlayerTwo);
            this.Controls.Add(this.textBoxPlayerOne);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButtonSize10);
            this.Controls.Add(this.radioButtonSize8);
            this.Controls.Add(this.radioButtonSize6);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonSize6;
        private System.Windows.Forms.RadioButton radioButtonSize8;
        private System.Windows.Forms.RadioButton radioButtonSize10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPlayerOne;
        private System.Windows.Forms.CheckBox checkBoxPlayerTwo;
        private System.Windows.Forms.TextBox textBoxPlayerTwo;
        private System.Windows.Forms.Button buttonDone;
    }
}