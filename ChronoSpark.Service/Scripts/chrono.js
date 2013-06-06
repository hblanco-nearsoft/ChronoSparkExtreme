(function () {
    
    var current,
        tasks = new kendo.data.DataSource({
            pageSize: 15,
            transport: {
                read: function(options) {
                    $.ajax({
                        url: "/home/gettasks",
                        dataType: "json", 
                        success: function(result) {                     
                            options.success(result);
                        },
                        error: function(result) {
                            options.error(result);
                        }
                    });
                },
                create: function (options) {          
                    var data = options.data,
                        task = {                            
                            Description: data.Description,
                            Duration: data.Duration,
                            Client: data.Client
                        };

                    $.ajax({
                        url: '/home/addtask',
                        type: 'POST',
                        contentType: 'application/json',
                        data: JSON.stringify(task)
                    }).done(function (datat) {
                        tasks.read();
                    })
                    .fail(function (err) { console.error(err); });
                       
                },
                update: function(options) {
                    var task = options.data;

                    $.ajax({
                        url: '/home/savechanges',
                        type: 'POST',
                        contentType: 'application/json',
                        data: JSON.stringify(task)
                    }).done(function (datat) {
                        tasks.read();
                    })
                    .fail(function (err) { console.error(err); });
                },
                destroy: function(options) {
                    var task = options.data;

                    $.ajax({
                        url: '/home/deletetask',
                        type: 'DELETE',
                        contentType: 'application/json',
                        data: JSON.stringify(task)
                    }).done(function (datat) {
                        tasks.read();
                    })
                    .fail(function (err) { console.error(err); });
                },

                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(data.models) }
                    }

                }
            },
            schema: {
                model: {
                    id: "Id",
                    fields: {
                        Id: { editable: false },
                        Description: { nullable: false, type: "string", validation: {required:true} },
                        Duration: { type: "number", validation: {required:true, min:1} },
                        TimeInHours: {editable:false},
                        Client: "Client"

                    }
                }
            }
        }),
        taskModel = new kendo.observable({
            tasks: tasks,            
            edit: function () { console.log("edit");},
            toJSON: function () {
                return {
                    Description:this.Description,
                    Duration: this.Duration,
                    Client: this.Client,
                    Id: this.Id,
                }
            }
        }),
        layout = new kendo.Layout("layout-template"),
        list = new kendo.View("list-template", { model: taskModel }),
        

        router = new kendo.Router({
            init: function () {
                layout.render("#main");
            }
        });    

    router.route('/', function () {
        layout.showIn("#list", list);
    });

    router.start();

    var listView = $("#tasks-list").kendoListView({
        pageable: true,
        editable: true,
        dataSource: tasks,
        template: kendo.template($("#task").html()),
        editTemplate: kendo.template($("#edit-template").html()),
        edit: function(){}
    }).data('kendoListView');

    $(".k-add-button").click(function (e) {
        listView.add();
        e.preventDefault();
    });



}())