﻿namespace ReporteMateriasOfertadas
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.universidadDBDataSet = new ReporteMateriasOfertadas.UniversidadDBDataSet();
            this.spReporteMateriasOfertadasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.spReporteMateriasOfertadasTableAdapter = new ReporteMateriasOfertadas.UniversidadDBDataSetTableAdapters.spReporteMateriasOfertadasTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.universidadDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteMateriasOfertadasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetReporteMateriasOfertadas";
            reportDataSource1.Value = this.spReporteMateriasOfertadasBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ReporteMateriasOfertadas.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 522);
            this.reportViewer1.TabIndex = 0;
            // 
            // universidadDBDataSet
            // 
            this.universidadDBDataSet.DataSetName = "UniversidadDBDataSet";
            this.universidadDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // spReporteMateriasOfertadasBindingSource
            // 
            this.spReporteMateriasOfertadasBindingSource.DataMember = "spReporteMateriasOfertadas";
            this.spReporteMateriasOfertadasBindingSource.DataSource = this.universidadDBDataSet;
            // 
            // spReporteMateriasOfertadasTableAdapter
            // 
            this.spReporteMateriasOfertadasTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 522);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.universidadDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteMateriasOfertadasBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource spReporteMateriasOfertadasBindingSource;
        private UniversidadDBDataSet universidadDBDataSet;
        private UniversidadDBDataSetTableAdapters.spReporteMateriasOfertadasTableAdapter spReporteMateriasOfertadasTableAdapter;
    }
}

