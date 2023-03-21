
//  var js = JQuery.noConflict(true);
var table;
$(document).ready(function () {
    table = $("#tbllist").DataTable({
        searching: false,
        ajax: '/Student/GetStudents',
        columns: [
            { data: 'stuId', 'visible': false },
            { data: 'name' },
            { data: 'fatherName' },
            { data: 'age' },
            { data: 'standard' },
            { data: 'uploadPic' },
            {
                data: 'stuId', render: function (data, type, row) {
                    return '<button type="button" class="btn btn-primary Edit_Btn" data-toggle="modal" data-target="#AddStudent" data-id="' + data + '"onclick="getForm(' + data + ')">Edit</button>  <button type="button" class="btn btn-danger Delete_Btn"onclick="Deletes(' + data + ')" data-id="' + data + '">Delete</button>';
                }
            }

        ]
    });
});

$('#savebtn').on('click', function () {

    var formData = {
        StuId: $("#stuId").val(),
        Name: $("#name").val(),
        FatherName: $("#fname").val(),
        Age: $("#age").val(),
        Standard: $("#standard").val(),
        UploadPic: $("#uploadpic").val(),
        ImageUrl: $("#imageurl").val(),
    };

    $.ajax({
        type: "POST",
        data: formData,
        url: "/Student/SaveStudent",
        dataType: "json",
        success: function (data) {
            //$('#AddStudent').hide();
            table.ajax.reload();
        },
        error: function () {
        }
    })
});

function Delete(data) {
    $.ajax({
        url: '/Student/Delete?id=' + data,
        dataType: "json",
        success: function (data) {
            table.ajax.reload();
        },
        error: function () {
        }
    });

}
//Delete Method with ajax
$(document).on('click', '.Delete_Btn', function () {
    let stuId = $(this).data('id');
    console.log(stuId);
    if (confirm('Are you Sure to delete?')) {
        Delete(stuId);
    } else {
        alert("not deleted");
        table.ajax.reload();
    }
});

//$.ajax({
//   url:'/Student/Delete?id='+stuId,
// //  data: stuId,
//   success: function(data){
//       alert(data);
//       table.ajax.reload(); 

//   },
//   error: function(){
//   }
// });


function getForm(id = 0) {
    $('#_student').load('/Student/StudentForm/' + id, () => {
        $("form").submit(function (e) {
            e.preventDefault();
            $.ajax({
                url: '/Student/Create',
                type: 'POST',
                data: $("form").serialize(),
                success: function (data) {
                    table.ajax.reload();
                    //ShowNotification(data,'dtActors','ActorsModal');
                }
            });
        });
    });
}


