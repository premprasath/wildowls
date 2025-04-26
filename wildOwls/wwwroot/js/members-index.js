let groupDataTable;
let memberDataTable;
var selectedRow = "";
var selectedFamilyRow = "";
var getFamilyUrl = "";
var selCount = 0;
var groupId1 = "";
var groupId2 = "";
var paymentDetails = "";
$(document).ready(function () {
    var getAllMembersUrl = baseUrl + "Members/GetAllGroupInfo";
    var columns = [
        {
            "data": "groupId",
            "title": "GroupId"
        },
        {
            "data": "groupIncharge",
            "title": "GroupIncharge"
        },
        {
            "data": "totalMembers",
            "title": "TotalMembers"
        },
        {
            "data": "couples",
            "title": "Couples"
        },
        {
            "data": "amountPaid",
            "title": "AmountPaid"
        },
        {
            "data": "groupId",
            "title": "View",
            "render": function (data) {
                return '<a class="btn btn-sm btn-primary" onclick="getMemberInfo(this);" style="min-width:70px;">View</a>';
            }
        }
    ];

    groupDataTable = $('#groupGrid').DataTable({
        ajax:
        {
            url: getAllMembersUrl,
            contentType: "application/json",
            dataSrc: "",
            type: "GET",
            data: function (result) {
                console.log(result);
                return JSON.stringify(result);
            },
            error: function (response) {
                var errorMessage = "Unable to get members details";
                if (response.statusText !== undefined) {
                    errorMessage = response.statusText;
                }
                DisplayErrorMessage(errorMessage);
            }
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "bSearchable": false, "aTargets": [1] },
            { "responsivePriority": 1, targets: [0] }
        ],
        "order": [],
        "aoColumns": columns,
        "responsive": true,
        "searching": false,
        "ordering": false,

        rowCallback: function (row, data) {
            if (data.groupIncharge === "Total") {
                $('td', row).css('background-color', '#ffcccb');
            }
        }
    });
});

function getMemberInfo(ctrl) {
    groupId1 === "";
    groupId2 === "";

    var rowCtrl = $(ctrl).parents('tr');
    selectedRow = groupDataTable.row(rowCtrl).data();

    getFamilyUrl = baseUrl + "Members/GetFamilyInfobyId?groupId=" + selectedRow.groupId;
    
    if (selCount == 1) {
        $("#membersGrid").dataTable().fnDestroy();
    }

    var columns = [
        {
            "data": "memberId",
            "title": "MemberId"
        },
        {
            "data": "familyId",
            "title": "FamilyId"
        },
        {
            "data": "memberName",
            "title": "MemberName"
        },
        {
            "data": "groupId",
            "title": "GroupId"
        },
        {
            "data": "phone",
            "title": "Phone"
        },
        {
            "data": "birthDate",
            "title": "BirthDate",
            "render": function (data) {
                if (data != null && data != "") {
                    if (data == "0001-01-01T00:00:00") {
                        data = "N/A"; return data;
                    }
                    else {
                        return '<span>' + moment(data).format("DD-MMM-YYYY") + '</span >';
                    }
                }
                else {
                    data = "N/A"; return data;
                }
            }
        },
        {
            "data": "marriageDate",
            "title": "MarriageDate",
            "render": function (data) {
                if (data != null && data != "") {
                    if (data == "0001-01-01T00:00:00") {
                        data = ""; return data;
                    }
                    else {
                        return '<span>' + moment(data).format("DD-MMM-YYYY") + '</span >';
                    }
                }
                else {
                    data = "N/A"; return data;
                }
            }
        },
        {
            "data": "amountPaid",
            "title": "AmountPaid"
        },
        {
            "data": "isAdditional",
            "title": "IsAdditional"
        },
        {
            "data": "familyId",
            "title": "Add",
            "render": function (data) {
                return '<button type="button" id="btnId" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#familyModal" onclick="setFamilyId(this);">Add</button>';
            }
        },
        {
            "data": "memberId",
            "title": "Edit",
            "render": function (data) {
                return '<button type="button" id="btnEditId" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#familyModal" onclick="editFamilyId(this);">Edit</button>';
            }
        }
    ];

    memberDataTable = $('#membersGrid').DataTable({
        ajax:
        {
            url: getFamilyUrl,
            contentType: "application/json",
            dataSrc: "",
            type: "GET",
            data: function (result) {
                selCount=1
                return JSON.stringify(result);
            },
            error: function (response) {
                var errorMessage = "Unable to get member details";
                if (response.statusText !== undefined) {
                    errorMessage = response.statusText;
                }
                DisplayErrorMessage(errorMessage);
            }
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "bSearchable": false, "aTargets": [0] },
            { "responsivePriority": 1, targets: [0] },
            {"visible": false, targets: [0,3,8]}
        ],
        "order": [],
        "aoColumns": columns,
        "responsive": true,
        "searching": false,
        "ordering": false,

        rowCallback: function (row, data, index) {

            //if (groupId1 === "") { groupId1 = data.familyGroupId; }

            //if (groupId1 === groupId2) {
            //    $('td', row).css('background-color', '#ffcccb');
            //    groupId1 = data.familyGroupId;
            //    groupId2 = data.familyGroupId;
            //}

            if (groupId1 === data.familyId) {
                $('td', row).css('background-color', '#ffcccb');
            }
            else {
                $('td', row).css('background-color', '#add8e6');
                groupId1 = data.familyId;
            }
        }
    });
}


function setFamilyId(ctrl) {
    var rowCtrl = $(ctrl).parents('tr');
    selectedFamilyRow = memberDataTable.row(rowCtrl).data();
    $('#familyId').val(selectedFamilyRow.familyId);
    $('#groupId').val(selectedFamilyRow.groupId);
    $('#isAdditional').val(1);
    $('#isEdit').val(0);

    $('#memberId').val(0);
    $('#memberName').val("");
    $('#phone').val("");
    $('#birthDate').val("");
    $('#marriageDate').val("");
    $('#amountPaid').val("");
    $('#address').val("");
}


function editFamilyId(ctrl) {
    var rowCtrl = $(ctrl).parents('tr');
    selectedFamilyRow = memberDataTable.row(rowCtrl).data();
    $('#familyId').val(selectedFamilyRow.familyId);
    $('#groupId').val(selectedFamilyRow.groupId);
    $('#isAdditional').val(1);
    $('#isEdit').val(1);


    $('#memberId').val(selectedFamilyRow.memberId);
    $('#memberName').val(selectedFamilyRow.memberName);
    $('#phone').val(selectedFamilyRow.phone);
    $('#birthDate').val(selectedFamilyRow.birthDate);
    $('#marriageDate').val(selectedFamilyRow.marriageDate);
    $('#amountPaid').val(selectedFamilyRow.amountPaid);
    $('#address').val(selectedFamilyRow.address);
}
