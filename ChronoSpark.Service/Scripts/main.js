(function chronoSparkMain($) {
    var $taskList = $('#tasks-list');


    function getAllTasks(e)
    {
        $.getJSON('home/gettasks')
         .done(function (data) {
             var idx = 0,
                 length = data.length,
                 form = '<ul class="actions"><input type="button" class="edit-btn"></input><input type="button" class="delete-btn"></input></ul>';
             for (; idx < length; idx += 1) {
                 var $item = $('<li/>');
                 $item.text(data[idx].Description);
                 // $item.append('<ul class="actions"><button class="edit-btn"></button><button class="delete-btn"></button></ul>');
                 $item.append(form);
                 $taskList.append($item);                 
             }
         }).fail(function (err) {
             console.error(err);
         });
    }

    function addTask(e)
    {
          var data = {
            Description: $('#description').val(),
            Duration: $('#duration').val(),
            Client: $('#client').val()
        };
        //$.post('home/addtask', JSON.stringify(data))
        //.done(function (data) {
        //    console.log(data);
        //})
        //.fail();

        $.ajax({
            url: 'http://localhost:8080/home/addtask',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (datat)
        {
            console.log(data);
        }
        )
        .fail(function (err) { console.erro(err); });    
    }

    function getbox() {
        Shadowbox.open({
            content: 'task/getbyid',
            player: "html",
            title: "Welcome",
            height: 350,
            width: 350
        });
    }
    //$( "#dialog" ).dialog({
    //    autoOpen: false,
    //    height: 300,
    //    width: 350,
    //    modal: true,
    //    buttons: {
    //        "Accept": function() {
    //        },
    //        Cancel: function () {
    //            $(this).dialog("close");
    //        }
    //    },
    //    close: function () {       
    //    }
    //});
    
    //$('.edit-btn').click(function (e)
    //{
    //      $( "#dialog" ).dialog( "open" );
    //});

    window.onload = function()
    {
        Shadowbox.open({
            content: ,
            player: "html",
            title: "Welcome",
            height: 350,
            width: 350
        });
    };
 

    /** Event Handling Setup*/
    $('#addtask').click(addTask);
    $(getAllTasks);
    //$('#getAllTasks').click(getAllTasks);
}(jQuery))
