$(document).ready(function () {
    var $subsidiaryForm = $("#subsidiaryForm");
    var $txtSubsidiary = $("#txtSubsidiary");
    var $taDescription = $("#taDescription");
    var $spinner = $("#spinner");

    var $subsidiaryTBody = $("#subsidiaryTBody");
    var $subsidiaryTemplate = $("#subsidiaryTemplate");

    //validation
    $subsidiaryForm.validate({
        rules: {
            name: {
                required: true,
                
            },
            description: {
                required: true
            }

        },
        submitHandler: function (form) {
            saveSubsidiary();
            return false;
        }
    });



    // get subsidiary data
    function subsidiaryData() {
        return {
            Name: $txtSubsidiary.val(),
            Description: $taDescription.val().trim()
        };
    }

    //submit subsidiary form
    function saveSubsidiary() {

        $spinner.removeClass("d-none");
        console.log(subsidiaryData());
        axios({
            method: 'post',
            url: "/api/subsidiary",
            data: subsidiaryData()
            })
            .then(function (res) {
                $spinner.addClass("d-none");

                console.log(res.data);
                notify("success", "Successfully saved subsidiary");
                location.reload();


            })
            .catch(function (err) {
                $spinner.addClass("d-none");

                console.log(err);
                notify("danger", "An error occured while savinign subsidiary");

            });
    }


    //get all subsidiaries
    getSubsidiaries().then(subsidiaries => {
        var template = $subsidiaryTemplate.html();
        var rendered = Mustache.render(template, { subsidiaries: subsidiaries });

        $subsidiaryTBody.html(rendered);
      
    });


    //delete subsidiaries
    $(document).on("click", ".delete", function () {
        var tr = $(this).closest("tr");
        var id = tr.data("id");
        var name = tr.find("td.name").text();

        deleteItemInTable(id, "api/user", name, tr);

    });

})