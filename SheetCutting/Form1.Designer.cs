namespace SheetCutting
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
            this.startAssemble = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.generateJson = new System.Windows.Forms.Button();
            this.prevStep = new System.Windows.Forms.Button();
            this.nextStep = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // startAssemble
            // 
            this.startAssemble.Location = new System.Drawing.Point(13, 14);
            this.startAssemble.Margin = new System.Windows.Forms.Padding(4);
            this.startAssemble.Name = "startAssemble";
            this.startAssemble.Size = new System.Drawing.Size(100, 28);
            this.startAssemble.TabIndex = 0;
            this.startAssemble.Text = "Assemble";
            this.startAssemble.UseVisualStyleBackColor = true;
            this.startAssemble.Click += new System.EventHandler(this.startAssemble_click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 49);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(996, 792);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // generateJson
            // 
            this.generateJson.Location = new System.Drawing.Point(495, 14);
            this.generateJson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.generateJson.Name = "generateJson";
            this.generateJson.Size = new System.Drawing.Size(133, 28);
            this.generateJson.TabIndex = 2;
            this.generateJson.Text = "Generate Json";
            this.generateJson.UseVisualStyleBackColor = true;
            this.generateJson.Click += new System.EventHandler(this.generateJson_Click);
            // 
            // prevStep
            // 
            this.prevStep.Location = new System.Drawing.Point(148, 14);
            this.prevStep.Name = "prevStep";
            this.prevStep.Size = new System.Drawing.Size(75, 28);
            this.prevStep.TabIndex = 3;
            this.prevStep.Text = "<";
            this.prevStep.UseVisualStyleBackColor = true;
            this.prevStep.Click += new System.EventHandler(this.prevStep_Click);
            // 
            // nextStep
            // 
            this.nextStep.Location = new System.Drawing.Point(229, 14);
            this.nextStep.Name = "nextStep";
            this.nextStep.Size = new System.Drawing.Size(75, 28);
            this.nextStep.TabIndex = 4;
            this.nextStep.Text = ">";
            this.nextStep.UseVisualStyleBackColor = true;
            this.nextStep.Click += new System.EventHandler(this.nextStep_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(310, 20);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(94, 17);
            this.label.TabIndex = 5;
            this.label.Text = "Current step: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 856);
            this.Controls.Add(this.label);
            this.Controls.Add(this.nextStep);
            this.Controls.Add(this.prevStep);
            this.Controls.Add(this.generateJson);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.startAssemble);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startAssemble;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button generateJson;
        private System.Windows.Forms.Button prevStep;
        private System.Windows.Forms.Button nextStep;
        private System.Windows.Forms.Label label;
    }
}

