--select 'TRUNCATE TABLE ' + TABLE_SCHEMA + '.' +  TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE='BASE TABLE'

TRUNCATE TABLE Master.ProductCategory
TRUNCATE TABLE Operation.InspectionDomestic
TRUNCATE TABLE Security.Users
TRUNCATE TABLE Config.SystemWideConfiguration
TRUNCATE TABLE Utility.DocumentNumberHeader
TRUNCATE TABLE Master.Customer
TRUNCATE TABLE Operation.InspectionOverSeas
TRUNCATE TABLE Master.QuotationItem
TRUNCATE TABLE Operation.OrderDetail
TRUNCATE TABLE Utility.DocumentNumberDetail
TRUNCATE TABLE Operation.GoodsReceiveDetail
TRUNCATE TABLE Master.Branch
TRUNCATE TABLE Security.Roles
TRUNCATE TABLE Utility.SearchColumn
TRUNCATE TABLE Master.Company
TRUNCATE TABLE Security.RoleRights
TRUNCATE TABLE Security.Securables
TRUNCATE TABLE Operation.InvoiceHeader
TRUNCATE TABLE Master.Country
TRUNCATE TABLE Master.Currency
TRUNCATE TABLE Operation.GoodsIssueDetail
TRUNCATE TABLE Operation.GoodsReceiveDetailsOverseas
TRUNCATE TABLE Master.Address
TRUNCATE TABLE Operation.StockLedger
TRUNCATE TABLE Operation.OrderHeader
TRUNCATE TABLE Operation.GoodsReceivePODetail
TRUNCATE TABLE Operation.PurchaseOrderHeader
TRUNCATE TABLE Master.CustomerProduct
TRUNCATE TABLE Master.Location
TRUNCATE TABLE Master.Product
TRUNCATE TABLE Operation.StockAdjustmentDetail
TRUNCATE TABLE Operation.SIHeaderHistory
TRUNCATE TABLE Operation.GoodsReceiveHeader
TRUNCATE TABLE Operation.PurchaseOrderHistory
TRUNCATE TABLE Master.ProductImage
TRUNCATE TABLE Operation.StockInDetail
TRUNCATE TABLE Master.Quotation
TRUNCATE TABLE Operation.PurchaseOrderDetail
TRUNCATE TABLE Operation.GoodsIssue
TRUNCATE TABLE Operation.SIHeader
TRUNCATE TABLE Operation.StockAdjustmentHeader
TRUNCATE TABLE Operation.InvoiceDetail
TRUNCATE TABLE Operation.StockInHeader
TRUNCATE TABLE Operation.SIDetail
TRUNCATE TABLE Config.Lookup