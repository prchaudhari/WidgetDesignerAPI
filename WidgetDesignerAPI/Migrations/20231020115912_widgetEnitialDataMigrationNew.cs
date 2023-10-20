using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WidgetDesignerAPI.API.Migrations
{
    /// <inheritdoc />
    public partial class widgetEnitialDataMigrationNew : Migration
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
                  { "Adhar Card Header", "Adhar Card Header (Government Of India)", "{\"src\":\"assets/sampleHeader.jpg\"}",
                 "2", "25", "<div ><img class=\"imgcss\" src={{:src}}></img></div>",
               "43b95ca3-b0d0-412e-b3b1-5c9abf9d4e41sampleHeader.jpg", "NULL", "NULL",
                 "AdharCardHeader", "fa fa-address-card", "0", "0" },

                   { "Photo", "Profile photo for Adharcard", "{\"src\":\"assets/adharProfile.jpg\"}", "2", "68",
               "<div ><img class=\"profilephoto\" src={{:src}}></img></div>", "fc1cf209-20da-4c27-8e17-ac535e2e786dadharphoto1.png",
               "NULL", "NULL",
                 "Photo", "fa fa-id-badge", "0", "0" },

                    { "Profile", "Body of Adharcard", "[{\"name\":\"Rohit\",\"dob\":\"01-01-2000\",\"gender\":\"male\",\"Address\":{\"Line1\":\"Address Line1\",\"Line2\":{\"city\":\"Mumbai\",\"state\":\"Maharashtra\"}},\"src\":\"assets/img.jpg\"}]",
               "1", "45", "<div class=\"imgcss\" >Name: - {{:name}}<br />DOB: - {{:dob}}<br />Gender:-{{:gender}}<br/>Address: - {{:Address.Line1}}<br/>{{:Address.Line2.city}}<br />{{:Address.Line2.state}}<br /></div>", "d534a72d-4265-459c-9a7c-49035d64520cabcd.png", "NULL", "NULL",
                 "AdharcardProfile", "fa fa-id-card", "0", "0" },

                     { "Adharcard Unique no", "Adharcard Unique Identity number", "[{\"adharnumber\":\"0000 1111 2222\"}]",
                     "1", "12", "<div class=\"adharnumbercss\">{{:adharnumber}}</div>", "1d836e0d-6b8c-48a3-99b3-16b4ebae27e9adharnumber.PNG",
                     "NULL", "NULL",
                   "AdharcardUniqueNo", "fa fa-id-card", "0", "0" },

                     { "Adharcard QR", "QR code for Unique Identification", "[{\"src\":\"assets/sampleAdharQr.jpg\"}]", "1", "100",
                      "<div><img class=\"imgcss\" src={{:src}}></img></div>",
                      "fdfaba2a-38f7-490f-affc-fa34f3d49790sampleAdharQr.jpg", "NULL", "NULL",
                        "AdharcardQR", "fa fa-id-card", "0", "0" },
        { "Satyamev Jayate Logo", "Logo of Satyamev Jayate", "{\"src\":\"assets/logosatya.jpg\"}", "1", "83",
                         "<div><img class=\"imgcss\" src={{:src}}></img></div>",
                 "9fbea43c-956b-4659-a582-617ca4268724logoSatyaMevJayate.PNG", "NULL", "NULL", "SatyamevJayateLogo", "fa fa-id-card ",
                 "0", "0" },
     { "adhar card footer", "adhar card footer", "{\"src\":\"assets/aamAdmi.jpg\"}", "2", "15",
                         "<div ><img class=\"imgcss\" src={{:src}}></img></div>",
                 "4a36d8ac55e2footer.jpg", "NULL", "NULL", "adhar card footer", "fa fa-address-card",
                 "0", "0" }
         });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
