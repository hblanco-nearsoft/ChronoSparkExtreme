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
             idx = length -1;
            $taskList.text('');
            for (; idx >= 0; idx -= 1) {
                var $task = data[idx],
                    $item = $('<li id = "' + $task.Id + '" duration = "'+ $task.Duration +'" />'),
                    $content = $('<ul class="actions"></ul>'),
                    $playButton = $('<li class="play-btn"/>'),
                    $pauseButton = $('<li class="pause-btn"/>'),
                    $displayDesc = $('<li id="desc"/>'),
                    $displayClient = $('<li id="clie"/>'),
                    $displayTime = $('<li id="elapsed"/>');
                //  $displayDur = $('<li id="dura"/>');


                $displayDesc.text($task.Description);
                $displayClient.text($task.Client);
                $displayTime.text($task.TimeInHours);
               // $displayDur.text($task.Duration);

                $content.append($displayDesc, $displayClient, $displayTime);
                $content.append('<li class="edit-btn">');

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
    //function getAllTasks(e) {
    //    $.getJSON('home/gettasks').done(function (data) {
           
    //    });

    //}
    
    //var dataSource = new kendo.data.DataSource({
    //    transport: {
    //        read: {
    //            url: "home/gettasks",
    //            dataType: "json",
    //        }
    //    },
    //    change: function () {

    //    },
    //    pageSize: 10
    //});

    //$taskList.kendoGrid({
    //    dataSource: dataSource,
    //    groupable: true,
    //    sortable: true,
    //    pageable: {
    //        regresh: true,
    //        pageSizes: true
    //    },
    //    columns: [
	//			"Description",
	//			"Duration",
	//			"Client",
    //            { command: { text: "View Details", click: addTask }, title: " ", width: "140px" }
    //    ],
    //    editable: "inline"

    //})

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
        if (!validateTask(data)) {
            return false;
        }

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

        return false;
    }

    function is_number(value) {
        return typeof value === "number" && isFinite(value);
    }

    function validateTask(e) {
        var $validation = $('#validation');

        $validation.text("");

        if (!e.Description) {
            $validation.append('<li>Description must be filled</li><br/>');
            return false;
        }
        if (!e.Duration) {
            $validation.append('<li>Duration must be filled</li><br/>');
            return false;
        }
        if (is_number(e.Duration)) {
            $validation.append('<li>Duration must be a number</li><br/>');
            return false;
        }
        if (e.Duration <= 0) {
            $validation.append('<li>Duration must be greater than 0</li><br/>')
            return false;
        }

        $validation.text("");
        return true;
    }

    function is_number(value) {
        return typeof value === "number" && isFinite(value);
    }

    function saveChanges(e) {
        e.preventDefault();
        var data = {
            Id: $('#save').parent().attr('id'),
            Description: $('#Description').val(),
            Duration: $('#Duration').val(),
            Client: $('#Client').val()
        };

        $.ajax({
            url: 'http://localhost:8080/home/savechanges',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (datat) {
            console.log(data);
            getAllTasks();
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
        $.getJSON('events/home/checkeventlist')
        .done(function (data) {
            var idx,
                len = data.length,
                $faceBoxDiv = $('<div/>'),
                $ok = $('<input id = "ok" type = "button" value = "Ok"/>'),
                $reminder;
            EventType = {
                IntervalPassed:0,
                NoActiveTask:1,
                EndOfWeek:2
            };
            for (idx = 0; idx < len ; idx += 1)
            {
                $reminder = data[idx];
                $faceBoxDiv.text($reminder.Message);
                $faceBoxDiv.append('</br>');
                $faceBoxDiv.append($ok);
                $.facebox($faceBoxDiv);
                $.titleAlert($reminder.Name);
                $faceBoxDiv.on('click', '#ok', function (e) {
                    $(document).trigger('close.facebox');
                })
            }
        })
        .fail(function (err) { console.error(err); });

        getAllTasks();
    }

    $editInput = $('<form class="editForm" ><label id="descLabel"/><input id="Description" required /><br/><label id="durLabel"/><input type="number" required id="Duration"/><br/><label id="clientLabel"/><input id="Client"/><br/><input type="submit" id="save" value="Update"/><input type="button" id="cancel" value="Cancel"/></form>');



    $('#descLabel', $editInput).text("Description: ");
    $('#durLabel', $editInput).text("Duration: ");
    $('#clientLabel', $editInput).text("Client: ");


    /** Event Handling Setup*/

    //$('#tasks-list > li > ul > input.edit-btn').on('click', function (e) { console.log($(this).parent().parent().text()); console.log('In Action Button'); });
    $taskList.on('click', 'li > ul > li.play-btn', function (e)
    {
        activateTask($(this).parent().parent().attr('id'));
    })

    $taskList.on('click', 'li > ul > li.pause-btn', function (e) {
        pauseTask();
    })

    $taskList.on('click', 'li > ul > li.edit-btn', function (e)
    {
        $('#Description', $editInput).val($(this).parent().children('#desc').text());
        $('#Duration', $editInput).val($(this).parent().parent().attr('duration'));
        $('#Client', $editInput).val($(this).parent().children('#clie').text());
        $.facebox($editInput);
        $('.editForm').attr('id', $(this).parent().parent().attr('id'));
        $editInput.submit(function (e) {
            saveChanges(e);
            $(document).trigger('close.facebox');
            getAllTasks();
        });
        $editInput.on('click', '#cancel', function (e) {
            $(document).trigger('close.facebox');
        });
    });

    $('#task-form').submit(addTask);
    $(getAllTasks);
    //$('#getAllTasks').click(getAllTasks);

    setInterval(function () { checkForReminders(); }, 60000);

}(jQuery))