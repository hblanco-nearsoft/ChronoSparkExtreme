(function chronoSparkMain($) {
    var $taskList = $('#tasks-list');


    function getAllTasks(e) {
        $.getJSON('home/gettasks')
         .done(function (data) {
             var idx = 0,
                 length = data.length;

             for (; idx < length; idx += 1) {
                 var $item = $('<li/>');

                 $item.text(data[idx].Description);
                 $taskList.append($item);
             }

         }).fail(function (err) {
             console.error(err);
         });
    }
    
    /** Event Handling Setup*/
    
    $('#getAllTasks').click(getAllTasks);
}(jQuery))
