namespace LW_3_4_Daryev1_MiA
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            urlTB = new TextBox();
            button1 = new Button();
            dogImage = new PictureBox();
            jsonTextDeserialiser = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dogImage).BeginInit();
            SuspendLayout();
            // 
            // urlTB
            // 
            urlTB.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            urlTB.Location = new Point(12, 37);
            urlTB.Name = "urlTB";
            urlTB.Size = new Size(700, 33);
            urlTB.TabIndex = 0;
            urlTB.ReadOnly = true;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(720, 37);
            button1.Name = "button1";
            button1.Size = new Size(104, 33);
            button1.TabIndex = 1;
            button1.Text = "Fetch";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dogImage
            // 
            dogImage.BackColor = SystemColors.ActiveBorder;
            dogImage.Location = new Point(463, 102);
            dogImage.Name = "dogImage";
            dogImage.Size = new Size(509, 401);
            dogImage.TabIndex = 2;
            dogImage.TabStop = false;
            dogImage.SizeMode = PictureBoxSizeMode.StretchImage;
            // 
            // jsonTextDeserialiser
            // 
            jsonTextDeserialiser.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            jsonTextDeserialiser.Location = new Point(12, 135);
            jsonTextDeserialiser.Multiline = true;
            jsonTextDeserialiser.Name = "jsonTextDeserialiser";
            jsonTextDeserialiser.Size = new Size(407, 368);
            jsonTextDeserialiser.TabIndex = 3;
            jsonTextDeserialiser.ReadOnly = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 102);
            label1.Name = "label1";
            label1.Size = new Size(67, 30);
            label1.TabIndex = 4;
            label1.Text = "JSON";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(984, 561);
            Controls.Add(label1);
            Controls.Add(jsonTextDeserialiser);
            Controls.Add(dogImage);
            Controls.Add(button1);
            Controls.Add(urlTB);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LW 3 Daryev";
            ((System.ComponentModel.ISupportInitialize)dogImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox urlTB;
        private Button button1;
        private PictureBox dogImage;
        private TextBox jsonTextDeserialiser;
        private Label label1;
    }
}
