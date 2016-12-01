CREATE TABLE "IngradientCategory" (
	"CategoryId" INT NOT NULL IDENTITY(1, 1),
	"CategoryName" VARCHAR(255) NOT NULL,
	PRIMARY KEY ("CategoryId")
);

CREATE TABLE "Ingradient" (
	"IngradientId" INT NOT NULL IDENTITY(1, 1),
	"IngradientName" VARCHAR(255) NOT NULL,
	"TotalWeight" INT NOT NULL,
	"CategoryId" INT NOT NULL,
	PRIMARY KEY ("IngradientId")
);

CREATE TABLE "ShawarmaRecipe" (
	"ShawarmaRecipeId" INT NOT NULL IDENTITY(1, 1),
	"ShawarmaId" INT NOT NULL,
	"IngradientId" INT NOT NULL,
	"Weight" INT NOT NULL,
	PRIMARY KEY ("ShawarmaRecipeId")
);

CREATE TABLE "Shawarma" (
	"ShawarmaId" INT NOT NULL IDENTITY(1, 1),
	"ShawarmaName" VARCHAR(255) NOT NULL,
	"CookingTime" INT NOT NULL,
	PRIMARY KEY ("ShawarmaId")
);

CREATE TABLE "OrderHeader" (
	"OrderHeaderId" INT NOT NULL IDENTITY(1, 1),
	"OrderDate" DATE NOT NULL,
	"SellerId" INT NOT NULL,
	PRIMARY KEY ("OrderHeaderId")
);

CREATE TABLE "OrderDetails" (
	"OrderHeaderId" INT NOT NULL,
	"ShawarmaId" INT NOT NULL,
	"Quantity" INT NOT NULL,
	PRIMARY KEY ("OrderHeaderId","ShawarmaId")
);

CREATE TABLE "PriceController" (
	"PriceControllerId" INT NOT NULL IDENTITY(1, 1),
	"ShawarmaId" INT NOT NULL,
	"Price" DECIMAL NOT NULL,
	"SellingPointId" INT NOT NULL,
	"Comment" TEXT NOT NULL,
	PRIMARY KEY ("PriceControllerId")
);

CREATE TABLE "Seller" (
	"SellerId" INT NOT NULL IDENTITY(1, 1),
	"SellerName" VARCHAR(255) NOT NULL,
	"SellingPointId" INT NOT NULL,
	PRIMARY KEY ("SellerId")
);

CREATE TABLE "SellingPoint" (
	"SellingPointId" INT NOT NULL IDENTITY(1, 1),
	"Address" varchar NOT NULL,
	"SellingPointCategoryId" INT NOT NULL,
	"ShawarmaTitle" varchar NOT NULL,
	PRIMARY KEY ("SellingPointId")
);

CREATE TABLE "TimeController" (
	"TimeControllerId" INT NOT NULL IDENTITY(1, 1),
	"SellerId" INT NOT NULL,
	"WorkStart" DATETIME NOT NULL,
	"WorkEnd" DATETIME NOT NULL,
	PRIMARY KEY ("TimeControllerId")
);

CREATE TABLE "SellingPointCategory" (
	"SellingPointCategoryId" INT NOT NULL IDENTITY(1, 1),
	"SellingPointCategoryName" VARCHAR(255) NOT NULL,
	PRIMARY KEY ("SellingPointCategoryId")
);

ALTER TABLE "Ingradient" ADD CONSTRAINT "Ingradient_fk0" FOREIGN KEY ("CategoryId") REFERENCES "IngradientCategory"("CategoryId");

ALTER TABLE "ShawarmaRecipe" ADD CONSTRAINT "ShawarmaRecipe_fk0" FOREIGN KEY ("ShawarmaId") REFERENCES "Shawarma"("ShawarmaId");

ALTER TABLE "ShawarmaRecipe" ADD CONSTRAINT "ShawarmaRecipe_fk1" FOREIGN KEY ("IngradientId") REFERENCES "Ingradient"("IngradientId");

ALTER TABLE "OrderHeader" ADD CONSTRAINT "OrderHeader_fk0" FOREIGN KEY ("SellerId") REFERENCES "Seller"("SellerId");

ALTER TABLE "OrderDetails" ADD CONSTRAINT "OrderDetails_fk0" FOREIGN KEY ("OrderHeaderId") REFERENCES "OrderHeader"("OrderHeaderId");

ALTER TABLE "OrderDetails" ADD CONSTRAINT "OrderDetails_fk1" FOREIGN KEY ("ShawarmaId") REFERENCES "Shawarma"("ShawarmaId");

ALTER TABLE "PriceController" ADD CONSTRAINT "PriceController_fk0" FOREIGN KEY ("ShawarmaId") REFERENCES "Shawarma"("ShawarmaId");

ALTER TABLE "PriceController" ADD CONSTRAINT "PriceController_fk1" FOREIGN KEY ("SellingPointId") REFERENCES "SellingPoint"("SellingPointId");

ALTER TABLE "Seller" ADD CONSTRAINT "Seller_fk0" FOREIGN KEY ("SellingPointId") REFERENCES "SellingPoint"("SellingPointId");

ALTER TABLE "SellingPoint" ADD CONSTRAINT "SellingPoint_fk0" FOREIGN KEY ("SellingPointCategoryId") REFERENCES "SellingPointCategory"("SellingPointCategoryId");

ALTER TABLE "TimeController" ADD CONSTRAINT "TimeController_fk0" FOREIGN KEY ("SellerId") REFERENCES "Seller"("SellerId");
