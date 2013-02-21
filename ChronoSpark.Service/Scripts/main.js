(function chronoSparkMain($) {
    var $taskList = $('#tasks-list');


    function getAllTasks(e)
    {
        $.getJSON('home/gettasks')
         .done(function (data) {
             var idx = 0,
                 length = data.length,
                 $form = '<ul class="actions"><input type="button" class="edit-btn"></input><input type="button" class="delete-btn"></input></ul>';
                  
             $taskList.text('');
             for (; idx < length; idx += 1) {
                 var $item = $('<li/>');
                 $item.text(data[idx].Description);
                 $item.append();
                 $item.append($form);
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
            getAllTasks();
            console.log(data);
        }
        )
        .fail(function (err) { console.erro(err); });    
    }

    function saveChanges(e) {
        var data = {
            Description: $('#description').val(),
            Duration: $('#duration').val(),
            Client: $('#client').val()
        };
        
        $.ajax({
            url: 'http://localhost:8080/home/savechanges',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (datat) {
            console.log(data);
        }
        )
        .fail(function (err) { console.erro(err); });
    }



    $editInput = $('<form><label id="descLabel"/><input id="Description" /><br/><label id="durLabel"/><input id="Duration"/><br/><label id="clientLabel"/><input id="Client"/><br/><input type="button" id="save" value="Update"/><input type="button" id="cancel" value="Cancel"/></form>');
    $('#descLabel', $editInput).text("Description: ");
    $('#durLabel', $editInput).text("Duration: ");
    $('#clientLabel', $editInput).text("Client: ");


    /** Event Handling Setup*/

    //$('#tasks-list > li > ul > input.edit-btn').on('click', function (e) { console.log($(this).parent().parent().text()); console.log('In Action Button'); });
    $taskList.on('click', 'li > ul > input.edit-btn', function (e)
    {
        $('#Description', $editInput).val($(this).parent().parent().text());
        $.facebox($editInput);
        $editInput.on('click', '#save', function (e) {
            saveChanges();
            $(document).trigger('close.facebox');
        });
        $editInput.on('click', '#cancel', function (e) {
            $(document).trigger('close.facebox');
        });
    });

   
    $('#addtask').click(addTask);
    $(getAllTasks);
    //$('#getAllTasks').click(getAllTasks);
    

}(jQuery))
