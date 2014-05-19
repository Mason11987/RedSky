namespace RedSkyContent
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTilePos = new System.Windows.Forms.Label();
            this.lblTile = new System.Windows.Forms.Label();
            this.picTile = new System.Windows.Forms.PictureBox();
            this.hscrTile = new System.Windows.Forms.HScrollBar();
            this.vscrTile = new System.Windows.Forms.VScrollBar();
            this.picTileSet = new System.Windows.Forms.PictureBox();
            this.lstTileSets = new System.Windows.Forms.ListBox();
            this.lblMapPos = new System.Windows.Forms.Label();
            this.btnChangeSize = new System.Windows.Forms.Button();
            this.txtMapHeight = new System.Windows.Forms.TextBox();
            this.txtMapWidth = new System.Windows.Forms.TextBox();
            this.vscrMap = new System.Windows.Forms.VScrollBar();
            this.hscrMap = new System.Windows.Forms.HScrollBar();
            this.picLayer = new System.Windows.Forms.PictureBox();
            this.lstLayers = new System.Windows.Forms.ListBox();
            this.lstMaps = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnAddLayer = new System.Windows.Forms.Button();
            this.btnRemoveLayer = new System.Windows.Forms.Button();
            this.chkDrawAll = new System.Windows.Forms.CheckBox();
            this.picMapTile = new System.Windows.Forms.PictureBox();
            this.tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTileSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMapTile)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Controls.Add(this.tabPage2);
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1737, 962);
            this.tabMain.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.picMapTile);
            this.tabPage1.Controls.Add(this.chkDrawAll);
            this.tabPage1.Controls.Add(this.btnRemoveLayer);
            this.tabPage1.Controls.Add(this.btnAddLayer);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.lblTilePos);
            this.tabPage1.Controls.Add(this.lblTile);
            this.tabPage1.Controls.Add(this.picTile);
            this.tabPage1.Controls.Add(this.hscrTile);
            this.tabPage1.Controls.Add(this.vscrTile);
            this.tabPage1.Controls.Add(this.picTileSet);
            this.tabPage1.Controls.Add(this.lstTileSets);
            this.tabPage1.Controls.Add(this.lblMapPos);
            this.tabPage1.Controls.Add(this.btnChangeSize);
            this.tabPage1.Controls.Add(this.txtMapHeight);
            this.tabPage1.Controls.Add(this.txtMapWidth);
            this.tabPage1.Controls.Add(this.vscrMap);
            this.tabPage1.Controls.Add(this.hscrMap);
            this.tabPage1.Controls.Add(this.picLayer);
            this.tabPage1.Controls.Add(this.lstLayers);
            this.tabPage1.Controls.Add(this.lstMaps);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1729, 936);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Map";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(149, 448);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTilePos
            // 
            this.lblTilePos.AutoSize = true;
            this.lblTilePos.Location = new System.Drawing.Point(1169, 358);
            this.lblTilePos.Name = "lblTilePos";
            this.lblTilePos.Size = new System.Drawing.Size(35, 13);
            this.lblTilePos.TabIndex = 16;
            this.lblTilePos.Text = "label1";
            // 
            // lblTile
            // 
            this.lblTile.AutoSize = true;
            this.lblTile.Location = new System.Drawing.Point(1210, 267);
            this.lblTile.Name = "lblTile";
            this.lblTile.Size = new System.Drawing.Size(16, 13);
            this.lblTile.TabIndex = 15;
            this.lblTile.Text = "...";
            // 
            // picTile
            // 
            this.picTile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picTile.Location = new System.Drawing.Point(1172, 248);
            this.picTile.Name = "picTile";
            this.picTile.Size = new System.Drawing.Size(32, 32);
            this.picTile.TabIndex = 14;
            this.picTile.TabStop = false;
            // 
            // hscrTile
            // 
            this.hscrTile.LargeChange = 1;
            this.hscrTile.Location = new System.Drawing.Point(1172, 891);
            this.hscrTile.Name = "hscrTile";
            this.hscrTile.Size = new System.Drawing.Size(505, 22);
            this.hscrTile.TabIndex = 13;
            this.hscrTile.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hscrTile_Scroll);
            // 
            // vscrTile
            // 
            this.vscrTile.LargeChange = 1;
            this.vscrTile.Location = new System.Drawing.Point(1687, 378);
            this.vscrTile.Name = "vscrTile";
            this.vscrTile.Size = new System.Drawing.Size(17, 510);
            this.vscrTile.TabIndex = 12;
            this.vscrTile.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vscrTile_Scroll);
            // 
            // picTileSet
            // 
            this.picTileSet.Location = new System.Drawing.Point(1172, 376);
            this.picTileSet.Name = "picTileSet";
            this.picTileSet.Size = new System.Drawing.Size(512, 512);
            this.picTileSet.TabIndex = 11;
            this.picTileSet.TabStop = false;
            this.picTileSet.Click += new System.EventHandler(this.picTileSet_Click);
            this.picTileSet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picTileSet_MouseDown);
            this.picTileSet.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picTileSet_MouseMove);
            // 
            // lstTileSets
            // 
            this.lstTileSets.FormattingEnabled = true;
            this.lstTileSets.Location = new System.Drawing.Point(1592, 3);
            this.lstTileSets.Name = "lstTileSets";
            this.lstTileSets.Size = new System.Drawing.Size(131, 277);
            this.lstTileSets.TabIndex = 10;
            this.lstTileSets.SelectedIndexChanged += new System.EventHandler(this.lstTileSets_SelectedIndexChanged);
            // 
            // lblMapPos
            // 
            this.lblMapPos.AutoSize = true;
            this.lblMapPos.Location = new System.Drawing.Point(1149, 3);
            this.lblMapPos.Name = "lblMapPos";
            this.lblMapPos.Size = new System.Drawing.Size(35, 13);
            this.lblMapPos.TabIndex = 9;
            this.lblMapPos.Text = "label1";
            // 
            // btnChangeSize
            // 
            this.btnChangeSize.Location = new System.Drawing.Point(226, 348);
            this.btnChangeSize.Name = "btnChangeSize";
            this.btnChangeSize.Size = new System.Drawing.Size(75, 23);
            this.btnChangeSize.TabIndex = 7;
            this.btnChangeSize.Text = "Resize";
            this.btnChangeSize.UseVisualStyleBackColor = true;
            this.btnChangeSize.Click += new System.EventHandler(this.btnChangeSize_Click);
            // 
            // txtMapHeight
            // 
            this.txtMapHeight.Location = new System.Drawing.Point(187, 352);
            this.txtMapHeight.Name = "txtMapHeight";
            this.txtMapHeight.Size = new System.Drawing.Size(32, 20);
            this.txtMapHeight.TabIndex = 6;
            // 
            // txtMapWidth
            // 
            this.txtMapWidth.Location = new System.Drawing.Point(149, 352);
            this.txtMapWidth.Name = "txtMapWidth";
            this.txtMapWidth.Size = new System.Drawing.Size(32, 20);
            this.txtMapWidth.TabIndex = 5;
            // 
            // vscrMap
            // 
            this.vscrMap.LargeChange = 1;
            this.vscrMap.Location = new System.Drawing.Point(1118, 18);
            this.vscrMap.Name = "vscrMap";
            this.vscrMap.Size = new System.Drawing.Size(17, 870);
            this.vscrMap.TabIndex = 4;
            this.vscrMap.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hscrMap
            // 
            this.hscrMap.LargeChange = 1;
            this.hscrMap.Location = new System.Drawing.Point(330, 891);
            this.hscrMap.Name = "hscrMap";
            this.hscrMap.Size = new System.Drawing.Size(785, 22);
            this.hscrMap.TabIndex = 3;
            this.hscrMap.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // picLayer
            // 
            this.picLayer.Location = new System.Drawing.Point(330, 19);
            this.picLayer.Name = "picLayer";
            this.picLayer.Size = new System.Drawing.Size(785, 869);
            this.picLayer.TabIndex = 2;
            this.picLayer.TabStop = false;
            this.picLayer.Click += new System.EventHandler(this.picLayer_Click);
            this.picLayer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picLayer_MouseDown);
            this.picLayer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picLayer_MouseMove);
            // 
            // lstLayers
            // 
            this.lstLayers.FormattingEnabled = true;
            this.lstLayers.Location = new System.Drawing.Point(149, 15);
            this.lstLayers.Name = "lstLayers";
            this.lstLayers.Size = new System.Drawing.Size(159, 277);
            this.lstLayers.TabIndex = 1;
            this.lstLayers.SelectedIndexChanged += new System.EventHandler(this.lstLayers_SelectedIndexChanged);
            // 
            // lstMaps
            // 
            this.lstMaps.FormattingEnabled = true;
            this.lstMaps.Location = new System.Drawing.Point(11, 15);
            this.lstMaps.Name = "lstMaps";
            this.lstMaps.Size = new System.Drawing.Size(131, 914);
            this.lstMaps.TabIndex = 0;
            this.lstMaps.SelectedIndexChanged += new System.EventHandler(this.lstMaps_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1729, 936);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnAddLayer
            // 
            this.btnAddLayer.Location = new System.Drawing.Point(149, 298);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new System.Drawing.Size(75, 23);
            this.btnAddLayer.TabIndex = 18;
            this.btnAddLayer.Text = "Add";
            this.btnAddLayer.UseVisualStyleBackColor = true;
            this.btnAddLayer.Click += new System.EventHandler(this.btnAddLayer_Click);
            // 
            // btnRemoveLayer
            // 
            this.btnRemoveLayer.Location = new System.Drawing.Point(233, 298);
            this.btnRemoveLayer.Name = "btnRemoveLayer";
            this.btnRemoveLayer.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveLayer.TabIndex = 19;
            this.btnRemoveLayer.Text = "Remove";
            this.btnRemoveLayer.UseVisualStyleBackColor = true;
            this.btnRemoveLayer.Click += new System.EventHandler(this.btnRemoveLayer_Click);
            // 
            // chkDrawAll
            // 
            this.chkDrawAll.AutoSize = true;
            this.chkDrawAll.Location = new System.Drawing.Point(149, 328);
            this.chkDrawAll.Name = "chkDrawAll";
            this.chkDrawAll.Size = new System.Drawing.Size(99, 17);
            this.chkDrawAll.TabIndex = 20;
            this.chkDrawAll.Text = "Draw All Layers";
            this.chkDrawAll.UseVisualStyleBackColor = true;
            this.chkDrawAll.CheckedChanged += new System.EventHandler(this.chkDrawAll_CheckedChanged);
            // 
            // picMapTile
            // 
            this.picMapTile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMapTile.Location = new System.Drawing.Point(1172, 19);
            this.picMapTile.Name = "picMapTile";
            this.picMapTile.Size = new System.Drawing.Size(32, 32);
            this.picMapTile.TabIndex = 21;
            this.picMapTile.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1761, 986);
            this.Controls.Add(this.tabMain);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "   ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTileSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMapTile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lstMaps;
        private System.Windows.Forms.ListBox lstLayers;
        private System.Windows.Forms.VScrollBar vscrMap;
        private System.Windows.Forms.HScrollBar hscrMap;
        private System.Windows.Forms.PictureBox picLayer;
        private System.Windows.Forms.Button btnChangeSize;
        private System.Windows.Forms.TextBox txtMapHeight;
        private System.Windows.Forms.TextBox txtMapWidth;
        private System.Windows.Forms.Label lblMapPos;
        private System.Windows.Forms.ListBox lstTileSets;
        private System.Windows.Forms.PictureBox picTileSet;
        private System.Windows.Forms.HScrollBar hscrTile;
        private System.Windows.Forms.VScrollBar vscrTile;
        private System.Windows.Forms.Label lblTile;
        private System.Windows.Forms.PictureBox picTile;
        private System.Windows.Forms.Label lblTilePos;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRemoveLayer;
        private System.Windows.Forms.Button btnAddLayer;
        private System.Windows.Forms.CheckBox chkDrawAll;
        private System.Windows.Forms.PictureBox picMapTile;
    }
}

