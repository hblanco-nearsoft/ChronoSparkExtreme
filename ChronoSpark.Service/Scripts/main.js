(function chronoSparkMain($) {
    var $taskList = $('#tasks-list');
    
    function getAllTasks(e)
    {
        $.getJSON('home/gettasks')
         .done(function (data) {
             var idx = 0,
                 length = data.length,
                 TaskState = {
                     Paused: 0,
                     InProgress: 1,
                     Finished: 2,
                     Reported: 3,
                 };
            $taskList.text('');
            for (; idx < length; idx += 1) {
                var $task = data[idx],
                    $item = $('<li id = "' + data[idx].Id + '"/>'),
                    $content = $('<ul class="actions"></ul>'),
                    $playButton = $('<input type="button" class="play-btn"/>'),
                    $pauseButton = $('<input type="button" class="pause-btn"/>'),
                    $displayDesc = $('<li id="desc"/>'),
                    $displayClient = $('<li id="clie"/>'),
                    $displayTime = $('<li id="elapsed"/>'),
                    $displayDur = $('<li id="dura"/>');

                $displayDesc.text($task.Description);
                $displayClient.text($task.Client);
                $displayTime.text($task.TimeInHours);
                $displayDur.text($task.Duration);

                $content.append($displayDesc, $displayDur ,$displayClient, $displayTime);
                $content.append('<input type="button" class="edit-btn">');

                if (data[idx].State == TaskState.InProgress) { $content.append($pauseButton); }
                if (data[idx].State == TaskState.Paused) { $content.append($playButton); }
                
                //$item.text(data[idx].Description);
                $item.append($content);
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
        })
        .fail(function (err) { console.error(err); });    
    }

    function saveChanges(e) {
        var data = {
            Id: $('#save').parent().attr('id'),
            Description: $('#Description').val(),
            //Duration: $('#duration').val(),
            Client: $('#Client').val()
        };
        
        $.ajax({
            url: 'http://localhost:8080/home/savechanges',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (datat) {
            console.log(data);
        })
        .fail(function (err) { console.error(err); });
    }

    function activateTask(e)
    {
        var data = {
            Id: e
        };
        $.ajax({
            url: 'http://localhost:8080/home/activatetask',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (datat) {
            console.log(data);
            getAllTasks();
        })
       .fail(function (err) { console.error(err); });

        console.log(data.Id);
    }

    function pauseTask()
    {
        $.post('home/pauseActiveTask')
        .done(function (data)
        {
            getAllTasks();
            console.log('paused');
        })
        .fail(function (err) { console.error(err); });   
    }

    function checkForReminders() {
        $.getJSON('event/checkeventlist')
        .done(function (data) {
            if (data.length == 0) { console.log('nothing to report');}
        })
        .fail(function (err) { console.error(err);});
    }

    $editInput = $('<form class="editForm" ><label id="descLabel"/><input id="Description" /><br/><label id="durLabel"/><input id="Duration"/><br/><label id="clientLabel"/><input id="Client"/><br/><input type="button" id="save" value="Update"/><input type="button" id="cancel" value="Cancel"/></form>');
    $('#descLabel', $editInput).text("Description: ");
    $('#durLabel', $editInput).text("Duration: ");
    $('#clientLabel', $editInput).text("Client: ");


    /** Event Handling Setup*/

    //$('#tasks-list > li > ul > input.edit-btn').on('click', function (e) { console.log($(this).parent().parent().text()); console.log('In Action Button'); });
    $taskList.on('click', 'li > ul > input.play-btn', function (e)
    {
        activateTask($(this).parent().parent().attr('id'));
    })

    $taskList.on('click', 'li > ul > input.pause-btn', function (e) {
        pauseTask();
    })

    $taskList.on('click', 'li > ul > input.edit-btn', function (e)
    {
        $('#Description', $editInput).val($(this).parent().children('#desc').text());
        $('#Duration', $editInput).val($(this).parent().children('#dura').text());
        $('#Client', $editInput).val($(this).parent().children('#clie').text());
        $.facebox($editInput);
        $('.editForm').attr('id', $(this).parent().parent().attr('id'));
        $editInput.on('click', '#save', function (e) {
            saveChanges(e);
            $(document).trigger('close.facebox');
            getAllTasks();
        });
        $editInput.on('click', '#cancel', function (e) {
            $(document).trigger('close.facebox');
        });
    });
       
    $('#addtask').click(addTask);
    $(getAllTasks);
    //$('#getAllTasks').click(getAllTasks);
    
   // setInterval(function () { checkForReminders(); }, 60000);

}(jQuery))
