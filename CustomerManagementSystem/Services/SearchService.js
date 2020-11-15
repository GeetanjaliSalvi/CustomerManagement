function searchClick() {
        // Get customer id from data- attribute
        var id = $("#firstname").val();

        // Store customer id in hidden field
        $("#firstname").val(id);

        // Call Web API to get a Customer
        $.ajax({
            url: '/CustomerManagementSystemAPI/api/GetCustomersByName/' + id,
            type: 'GET',
            headers: { 'X-CustomerManagementApiKey': 'CustomerManagementX123:YesAppKeyIsPersist' },
            dataType: 'json',
            success: function (customers) {
                CustomerListSuccess(customers);
        
            },
            error: function (request, message, error) {
                handleException(request, message, error);
            }
        });
    }
    
// Display all Customers returned from Web API call
function CustomerListSuccess(customers) {

    // Iterate over the collection of data
    $.each(customers, function (index, customer) {
        // Add a row to the Customer table
        CustomerAddRow(customer);
    });
}
// Add Customer row to <table>
function CustomerAddRow(customer) {
    // First check if a <tbody> tag exists, add one if not
    if ($("#CustomerTable tbody").length === 0) {
        $("#CustomerTable").append("<tbody></tbody>");
           
    }
        
    // Append row to <table>
    $("#CustomerTable tbody").append(
      CustomerBuildTableRow(customer));
}

// Build a <tr> for a row of table data
function CustomerBuildTableRow(customers) {
    var ret =
        "<tr>" +
            "<td>" +
            customers.FirstName +
            "</td>" +
            "<td>" +
            customers.LastName +
            "</td>" +
            "<td>" +
            customers.PhoneNumber +
            "</td>" +
            "<td>" +
            customers.Email +
            "</td>";
    if (customers.IsActive) {
        ret = ret + "<td><input type=\"checkbox\" checked disabled></td>";
    } else
        ret = ret + "<td><input type=\"checkbox\" disabled></td>";
    ret = ret + "</tr>";
    return ret;
}

// Handle exceptions from AJAX calls
function handleException(request, message, error) {
    var msg = "";

    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON != null) {
        msg += "Message" + request.responseJSON.Message + "\n";
    }

    alert(msg);
}
