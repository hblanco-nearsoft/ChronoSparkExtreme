(function chronoSparkMain($) {
    var $taskList = $('#tasks-list');


    function getAllTasks(e)
    {
        $.getJSON('home/gettasks')
         .done(function (data) {
             var idx = 0,
                 length = data.length;

             for (; idx < length; idx += 1) {
                 var $item = $('<li/>');
                 $item.text(data[idx].Description + " " + data[idx].Client);
                 $item.append('<ul class="actions"><button class="edit-btn"><a href="#"></a></button><button class="delete-btn"><a href="#"></a></button></ul>');
                 $taskList.append($item);                 
             }

         }).fail(function (err) {
             console.error(err);
         });
    }

    function addTask()
    {
        var formData = $(this).serialize();

        $.post(
            '<%= Url.Action("addTask") %>',
            formData,
            processResult       
        );
        event.preventDefault();
    }


    /** Event Handling Setup*/
    $('#task-form').submit(addTask);
    $(getAllTasks);
    $('#getAllTasks').click(getAllTasks);
}(jQuery))
