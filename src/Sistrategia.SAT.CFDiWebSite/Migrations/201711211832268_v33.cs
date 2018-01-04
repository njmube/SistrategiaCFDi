namespace Sistrategia.SAT.CFDiWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v33 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.sat_concepto_impuestos",
                c => new
                    {
                        impuestos_id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.impuestos_id);
            
            CreateTable(
                "dbo.sat_concepto_impuesto_retencion",
                c => new
                    {
                        retencion_id = c.Int(nullable: false, identity: true),
                        _base = c.Decimal(name: "base", nullable: false, precision: 18, scale: 2),
                        impuesto = c.String(),
                        tipo_factor = c.String(),
                        tasa_o_cuota = c.Decimal(precision: 18, scale: 2),
                        importe = c.Decimal(precision: 18, scale: 2),
                        ordinal = c.Int(),
                        impuesto_id = c.Int(),
                    })
                .PrimaryKey(t => t.retencion_id)
                .ForeignKey("dbo.sat_concepto_impuestos", t => t.impuesto_id)
                .Index(t => t.impuesto_id);
            
            CreateTable(
                "dbo.sat_concepto_impuesto_traslado",
                c => new
                    {
                        traslado_id = c.Int(nullable: false, identity: true),
                        _base = c.Decimal(name: "base", nullable: false, precision: 18, scale: 2),
                        impuesto = c.String(),
                        tipo_factor = c.String(),
                        tasa_o_cuota = c.Decimal(precision: 18, scale: 2),
                        importe = c.Decimal(precision: 18, scale: 2),
                        ordinal = c.Int(),
                        impuesto_id = c.Int(),
                    })
                .PrimaryKey(t => t.traslado_id)
                .ForeignKey("dbo.sat_concepto_impuestos", t => t.impuesto_id)
                .Index(t => t.impuesto_id);
            
            AddColumn("dbo.sat_comprobante", "forma_pago", c => c.String(maxLength: 2));
            AddColumn("dbo.sat_comprobante", "metodo_pago", c => c.String(maxLength: 3));
            AddColumn("dbo.sat_comprobante", "confirmacion", c => c.String(maxLength: 5));
            AddColumn("dbo.sat_concepto", "impuestos_id", c => c.Int());
            AddColumn("dbo.sat_regimen_fiscal", "regimen_fiscal_clave", c => c.String());
            AddColumn("dbo.sat_traslado", "tipo_factor", c => c.String());
            AddColumn("dbo.sat_traslado", "tasa_o_cuota", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.sat_receptor", "residencia_fiscal", c => c.String());
            AddColumn("dbo.sat_receptor", "num_reg_id_trib", c => c.String());
            AddColumn("dbo.sat_receptor", "uso_cfdi", c => c.String());
            AlterColumn("dbo.sat_comprobante", "forma_de_pago", c => c.String(maxLength: 256));
            AlterColumn("dbo.sat_traslado", "tasa", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.sat_concepto", "impuestos_id");
            AddForeignKey("dbo.sat_concepto", "impuestos_id", "dbo.sat_concepto_impuestos", "impuestos_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sat_concepto", "impuestos_id", "dbo.sat_concepto_impuestos");
            DropForeignKey("dbo.sat_concepto_impuesto_traslado", "impuesto_id", "dbo.sat_concepto_impuestos");
            DropForeignKey("dbo.sat_concepto_impuesto_retencion", "impuesto_id", "dbo.sat_concepto_impuestos");
            DropIndex("dbo.sat_concepto_impuesto_traslado", new[] { "impuesto_id" });
            DropIndex("dbo.sat_concepto_impuesto_retencion", new[] { "impuesto_id" });
            DropIndex("dbo.sat_concepto", new[] { "impuestos_id" });
            AlterColumn("dbo.sat_traslado", "tasa", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.sat_comprobante", "forma_de_pago", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.sat_receptor", "uso_cfdi");
            DropColumn("dbo.sat_receptor", "num_reg_id_trib");
            DropColumn("dbo.sat_receptor", "residencia_fiscal");
            DropColumn("dbo.sat_traslado", "tasa_o_cuota");
            DropColumn("dbo.sat_traslado", "tipo_factor");
            DropColumn("dbo.sat_regimen_fiscal", "regimen_fiscal_clave");
            DropColumn("dbo.sat_concepto", "impuestos_id");
            DropColumn("dbo.sat_comprobante", "confirmacion");
            DropColumn("dbo.sat_comprobante", "metodo_pago");
            DropColumn("dbo.sat_comprobante", "forma_pago");
            DropTable("dbo.sat_concepto_impuesto_traslado");
            DropTable("dbo.sat_concepto_impuesto_retencion");
            DropTable("dbo.sat_concepto_impuestos");
        }
    }
}
