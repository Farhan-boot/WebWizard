
function btnNewsFeed() {
    
$.ajax({
    type: 'GET',
    datatype: 'json',
    contentType: "application/json; charset=utf-8", 
    url: '/WizerdPartialView/PartialViewMyNewsFeed',
    success: function (resp) {
        var divison;
        for (var i in JSON.stringify(resp)) {
            divison = i;
            
        }
        $('#contentView').append(divison);



       // var a = JSON.stringify(resp);
       
       // var c = resp;
       

      // $("#contentView").html(resp);

        
       

    },
    error: function (resp) {
        console.log(resp);
    }
    });


   
}