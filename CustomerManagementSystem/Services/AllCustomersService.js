// Get all Customers to display
function CustomerList() {
    // Call Web API to get a list of Customers
    $.ajax({
        url: '/CustomerManagementSystemAPI/api/GetCustomers/',

        type: 'GET',
        headers: { 'X-CustomerManagementApiKey': 'CustomerManagementX123:YesAppKeyIsPersist' },
        dataType: 'json',

        success: function (Customers) {
            CustomerListSuccess(Customers);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

// Display all Customers returned from Web API call
function CustomerListSuccess(Customers) {
    // Iterate over the collection of data
    $.each(Customers,
        function (index, Customer) {
            // Add a row to the Customer table
            CustomerAddRow(Customer);
        });
}

// Add Customer row to <table>
function CustomerAddRow(Customer) {
    // First check if a <tbody> tag exists, add one if not
    if ($("#CustomerTable tbody").length == 0) {
        $("#CustomerTable").append("<tbody></tbody>");
    }

    // Append row to <table>
    $("#CustomerTable tbody").append(
        CustomerBuildTableRow(Customer));
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