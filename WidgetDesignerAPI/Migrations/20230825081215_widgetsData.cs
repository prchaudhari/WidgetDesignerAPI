using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WidgetDesignerAPI.API.Migrations
{
    /// <inheritdoc />
    public partial class widgetsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
       table: "Widgets",
       columns: new[] { "WidgetName", "Description", "DataSourceJson", "Width", "Height", "WidgetHtml", "WidgetIconUrl",
             "WidgetCSSUrl", "WidgetCSS", "DataBindingJsonNode", "FontName", "StartCol", "StartRow" },
       values: new object[,]
       {
                { "Satya Mev Logo", "Satya Mev Jayate", "[{\"src\":\"assets/satya.jpg\"}]", "1", "2", "{{for abc}}<div><img src={{:src}}></img></div>{{/for}}",
                 "7630a626-0769-4b7f-8cd6-2c8e79abd94cSatyaMev.PNG", "NULL", "NULL", "SatyaMevLogo", "fa fa-address-card", "0", "0" },
                  { "Adhar Card Header", "Adhar Card Header (Government Of India)", "[{\"src\":\"assets/sampleHeader.jpg\"}]",
                 "1", "2", "<div><img src={{:src}}></img></div>", "43b95ca3-b0d0-412e-b3b1-5c9abf9d4e41sampleHeader.jpg", "NULL", "NULL",
                 "AdharCardHeader", "fa fa-address-card", "0", "0" },
                   { "Adharcard Profile", "Profile photo for Adharcard", "[{\"src\":\"assets/adharProfile.jpg\"}]", "2", "2", "<div class=\"profilephoto\"><img src={{:src}}></img></div>", "fc1cf209-20da-4c27-8e17-ac535e2e786dadharphoto1.png", "NULL", "NULL",
                 "AdharcardProfile", "fa fa-id-badge", "0", "0" },
                    { "Profile", "Body of Adharcard", "[{\"Name\":\"Amit\",\"dob\":\"01-01-2000\",\"gender\":\"male\",\"Address\":{\"Line1\":\"Address Line1\",\"Line2\":{\"city\":\"Mumbai\",\"state\":\"Maharashtra\"}},\"src\":\"assets/img.jpg\"}]", "1", "2", "<div>name: - {{:name}}<br />DOB: - {{:dob}}<br />Gender:-{{:gender}}<br/>Address: - {{:Address.Line1}}<br/>{{:Address.Line2.city}}<br />{{:Address.Line2.state}}<br /></div>", "3a8a561b-f4ea-4c43-abf5-6e7aa2d27fc5profile.PNG", "NULL", "NULL",
                 "AdharcardProfile", "fa fa-id-card", "0", "0" },
                     { "Adharcard Unique no", "Adharcard Unique Identity number", "[{\"adharnumber\":\"0000 1111 2222\"}]", "1", "2", "<div class=\"adharnumbercss\">{{:adharnumber}}</div>", "1d836e0d-6b8c-48a3-99b3-16b4ebae27e9adharnumber.PNG", "NULL", "NULL",
                 "AdharcardUniqueNo", "fa fa-id-card", "0", "0" },
                      { "Adharcard QR", "QR code for Unique Identification", "[{\"src\":\"assets/sampleAdharQr.jpg\"}]", "1", "2", "<div><img src={{:src}}></img></div>", "fdfaba2a-38f7-490f-affc-fa34f3d49790sampleAdharQr.jpg", "NULL", "NULL",
                 "AdharcardQR", "fa fa-id-card", "0", "0" }

         });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
