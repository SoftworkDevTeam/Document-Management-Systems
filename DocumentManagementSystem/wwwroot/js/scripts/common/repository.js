


$(document).ready(function () {

    $.validator.setDefaults({
        errorClass: "help-block",
        highlight: function (element) {
            $(element)
                .closest(".form-group")
                .addClass("text-danger");
        },
        unhighlight: function (element) {
            $(element)
                .closest(".form-group")
                .removeClass("text-danger");
        }
    });
});

var $deleteModal = $("#deleteModal");
var $deleteButton = $("#deleteButton");
var $deleteModalBody = $("#deleteModalBody");



const PRESCHEDULED = "PreScheduled";
const CHECKEDIN = "CheckedIn";
const CHECKEDOUT = "CheckedOut";



// Get All Subsidiaries
function getSubsidiaries() {
    return axios.get("/api/subsidiary").then(res => res.data);
}

//Get all employee type
function getEmployeeTypes() {
    return axios.get("/api/employeetype").then(res => res.data);
}

//Get all roles 
function getRoles() {
    return axios.get("/api/profile/roles").then(res => res.data);
}

//Get all user profile 
function getUsers() {
    return axios.get("/api/profile").then(res => res.data);
}

//Get all device color
function getDeviceColor() {
    return axios.get("/api/devicecolor").then(res => res.data);
}
//Get all device model
function getDeviceModel() {
    return axios.get("/api/devicemodel").then(res => res.data);
}
//Get all device type
function getDeviceType() {
    return axios.get("/api/devicetype").then(res => res.data);
}

//Get all property type
function getDevicePropertyType() {
    return axios.get("/api/devicePropertyType").then(res => res.data);
}
//Get all Device
function getDevices() {
    return axios.get("/api/device").then(res => res.data);
}

//Get all visitory
function getVisitors() {
    return axios.get("/api/visitor").then(res => res.data);
}

//Get Today visitory
function getTodayVisitors() {
    return axios.get("/api/visitor/today").then(res => res.data);
}
//Get all visitory by host id
function getVisitorsByHostId(hostId) {
    return axios.get("/api/visitor/host/" + hostId).then(res => res.data);
}
//get all visitors with the same status
function getVisitorsByStatus(statusName) {
    return axios.get("/api/visitor/status/" + statusName).then(res => res.data);

}

//Get visitors by start and end date
function getVisitorByStartEndDate(startDate, endDate) {
    return axios.get("/api/visitor/date/" + startDate + "/" + endDate).then(res => res.data);
}
//Get todays device inventory
function getInventoriesForToday() {
    return axios.get("/api/inventory/today").then(res => res.data);
}
//Get todays device owner inventory
function getInventoriesForTodayOwner(ownerId) {
    return axios.get("/api/inventory/today/" + ownerId).then(res => res.data);
}

//Get inventory by start and end date
function getInventoryByStartEndDate(startDate, endDate) {
    return axios.get("/api/inventory/date/" + startDate + "/" + endDate).then(res => res.data);
}
//Get inventory by onwer's start and end date
function getInventoryByOwnerStartEndDate(ownerId, startDate, endDate) {
    return axios.get("/api/inventory/date/" + ownerId + "/" + startDate + "/" + endDate).then(res => res.data);
}

function getProfileByToken(token) {
    return axios.get("/api/auth/token/" + token).then(res => res.data);

}

//Get todays device Movement for each user
function getMovementForTodayByUser(userId) {
    return axios.get("/api/inventory/today/" + userId).then(res => res.data);
}
//include index in data
function includeIndex(data) {
    return data.map(d => d = { index: data.indexOf(d) + 1, ...d });
}

//notification function
function notify(type, message) {
    $.notify({
        // options
       
        title: "<center><i class='fa fa-user'></i> Notification</center>",
        message: message
    }, {
            // settings
            type: type

        });
};

function deleteItem(className, url) {
    $(document).on("click", className, function () {
        var tr = $(this).closest("tr");
        var id = tr.data("id");
        var name = tr.find("td.name").text();


        deleteItemInTable(id, url, name, tr);
        
    });
};

//delete item in table
function deleteItemInTable(id, url, item, rowObject) {
    $deleteModalBody.html('<div class="alert alert-light">Are you sure you want to delete <strong><em> ' + item + '</em></strong>?</div><br/><small class="red-text"> please note that items with existing inventory cannot be deleted</small>');

    $deleteButton.click(function () {
        $deleteModalBody.html('<div class="alert alert-warning">deleting <strong><em> ' + item + '</em></strong>...</div>');
        console.log(id);
        if (!!id) {


            axios({
                method: "delete",
                url: url + "/" + id

            }).then(res => {
                if (res.data === "item not found") {
                    $deleteModalBody.html('<div class="alert alert-info">Item selected Does not Exist<strong><em> ' + item + '</em></strong>.</div>');

                } else {
                    $deleteModalBody.html('<div class="alert alert-success">Item successfully deleted<strong><em> ' + item + '</em></strong>.</div>');
                    $deleteModal.modal("hide");
                    rowObject.remove();
                }

                console.log(res);
            }).catch(err => {
                $deleteModalBody.html('<div class="alert alert-danger">Error deleting <strong><em> ' + item + '</em></strong>?</div>');

            });

        }


    });

    //Unbind datepicker selector
    function unbindDatePicker(selector) {
        selector.removeClass('calendarclass');
        selector.removeClass('hasDatepicker');
        selector.unbind();
    }



}

