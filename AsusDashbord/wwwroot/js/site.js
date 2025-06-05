var table = document.getElementById("table");
var Settable = document.getElementById("Settable");
var themeButton = document.getElementById("themeButton");
var RoleCardIndex = document.getElementById("RoleCardIndex");
// Function to update the table color based on the body class
function updateTableColor() {
    if (document.body.classList.contains("darkmode")) {
        table.style.color = "white";
        RoleCardIndex.style.backgroundColor = "#495057";
    } else {
        table != null? table.style.color = "black":"";
        RoleCardIndex.style.backgroundColor = "#e9ecef";
    }
}
// Initial check on page load
updateTableColor();

// Event listener for the theme button
themeButton.onclick = function () {
    document.body.classList.toggle("darkmode");
    updateTableColor();
};
// Optional: Listen for changes to the body class (if needed)
document.body.addEventListener('classChange', updateTableColor);
//========================
//for role create on model
function CreateRole() {
    $.ajax({
        url: `/Role/CreateRole`,
        type: 'GET',
        success: function (res) {
            $('#CreatContent').html(res);
        }
    });
}
//========================
//for role Update on model
function UpdateRole(id) {
    $.ajax({
        url: `/Role/UpdateRole/${id}`,
        type: 'GET',
        success: function (res) {
            $('#CreatContent').html(res);
        }
    });
}
//========================
//for role delete on model
function DeleteRole(id) {
    $.ajax({
        url: `/Role/DeleteRole/${id}`,
        type: 'GET',
        success: function (res) {
            $.ajax({
                url: `/Role/Index`,
                type: 'GET',
            });
        }
    });
}
//==============================
//========================
//for user update on model
function UpdateUser(id) {
    $.ajax({
        url: `/Account/Update/${id}`,
        type: 'GET',
        success: function (res) {
            $('#CreatContentForUser').html(res);
        }
    });
}
//========================
//for user update on model
function DeleteUser(id) {
    $.ajax({
        url: `/Account/Delete/${id}`,
        type: 'GET',
        success: function (res) {
            //$('#Settable').html(res);
            $("#Settable").load(window.location.href + " #Settable > *");
        }
    });
}
//========================
// add role to user   
function AddRoleToUser(id) {
    $.ajax({
        url: `/Account/AddRoleToUser/${id}`,
        type: 'GET',
        success: function (res) {
            $('#UserRoleContent').html(res);
        }
    });
}
//==========================
//**********************
// for update index table only
function UpdateIndex(id) {
    $.ajax({
        url: `/Account/PartialIndex/${id}`,
        type: 'GET',
        success: function (res) {
            $('#Settable').html(res);
        },
        error: function (err) {
            console.log(err)
        }
    });
}
//=============================
// for user index search 
function performSearch() {
    const id = document.getElementById('searchInput').value;
    if (id != "") {
        UpdateIndex(id);
    }
}
//======================
// for user index search 
function Reset() {
    const id = document.getElementById('searchInput');
    // id.Reset;
    id.value = "";
    UpdateIndex(id.value);
    //$.ajax({
    //    url: `/Account/PartialIndex/${id}`,
    //    type: 'GET',
    //    success: function (res) {
    //        $('#Settable').html(res);
    //    },
    //    error: function (err) {
    //        console.log(err)
    //    }
    //});
    //window.location.href = '/Account/Index?pageNumber=1' + '&PageSize=15';
}
//===========================
// Previous page 
function PreviousPage(number) {
    var pageNumber = number - 1;
    window.location.href = '/Account/Index?pageNumber=' + pageNumber + '&PageSize=15';
}
//==========================
//************************
//Next Page
function NextPage(number) {
    var pageNumber = number + 1;
    window.location.href = '/Account/Index?pageNumber=' + pageNumber + '&PageSize=15';
}
//================================
//*************************
// function for reload 
function Reload() {
    UpdateIndex("");
}
//==========================
//**********************
// for update index table only
function UpdateProductIndex(id) {
    $.ajax({
        url: `/Product/PartialIndex/${id}`,
        type: 'GET',
        success: function (res) {
            $('#Settable').html(res);
        },
        error: function (err) {
            console.log(err)
        }
    });
}
//=============================
// for user index search 
function performProductSearch() {
    const id = document.getElementById('searchInput').value;
    if (id != "") {
        UpdateProductIndex(id);
    }
}
//======================
// for user index search 
function ResetProduct() {
    const id = document.getElementById('searchInput');
    // id.Reset;
    id.value = "";
    UpdateProductIndex(id.value); 
}
//================================
//*************************
// function for reload  Product
function ReloadProduct() {
    UpdateIndex("");
}

//==========================
//**********************
// for update index table only
function UpdateOrderIndex(id) {
    $.ajax({
        url: `/Order/PartialIndex/${id}`,
        type: 'GET',
        success: function (res) {
            $('#Settable').html(res);
        },
        error: function (err) {
            console.log(err)
        }
    });
}
//=============================
// for user index search 
function performOrderSearch() {
    const id = document.getElementById('searchInput').value;
    if (id != "") {
        UpdateOrderIndex(id);
    }
}
//======================
function UpdateOrderState(id, OrderStateId) {
    $.ajax({
        url: `/Order/UpdateOrderState`,
        type: 'POST',
        data: {
            id: id,
            OrderStateId: OrderStateId
        },
        success: function (res) {
            UpdateOrderIndex("");
            // Get the modal element
            var myModal = new bootstrap.Modal(document.getElementById('UserRole'));

            myModal.hide();

        },
        error: function (xhr, status, error) {
            console.error("Error updating order state: ", error);
        }
    });
}
//======================
// for user index search 
function ResetOrder() {
    const id = document.getElementById('searchInput');
    // id.Reset;
    id.value = "";
    UpdateOrderIndex(id.value);
}
//================================
//*************************
// function for reload  Product
function ReloadOrder() {
    UpdateOrderIndex("");
}
//==================================

function PreviousOP(number) {
    var pageNumber = number - 1;
    window.location.href = '/Order/Index?pageNumber=' + pageNumber + '&PageSize=15';
}
//==========================
//************************
//Next Page
function NextOP(number) {
    var pageNumber = number + 1;
    window.location.href = '/Order/Index?pageNumber=' + pageNumber + '&PageSize=15';
}