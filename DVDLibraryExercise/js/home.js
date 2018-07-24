$(document).ready(function () {

  loadDvds();

  $('#create-button').click(function(event){
    showCreateDisplay();
  });

  $("#create-form-button").click(function(event){

    var haveValidationErrors = checkAndDisplayValidationErrors($('#create-form').find('input'));

    if (haveValidationErrors) {
        return false;
    }

    $.ajax({
      type: 'POST',
      url: 'http://localhost:52939/dvd',
      data: JSON.stringify({
          title: $('#create-title').val(),
          realeaseYear: $('#create-release-year').val(),
          director: $('#create-director').val(),
          rating: $('#create-rating').val(),
          notes: $('#create-notes').val()
      }),
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      'dataType': 'json',
      success: function() {
        $('#errorMessages').empty();
        $('#create-title').val('');
        $('#create-release-year').val('');
        $('#create-director').val('');
        $('#create-rating').val('');
        $('#create-notes').val('');
        $('#searchContentRows').hide();
        $('#searchContentRows').empty();
        $('#contentRows').show();
        loadDvds();
        showHomeDisplay();

      },
      error: function() {
        $("#errorMessages")
          .append($('<li>')
          .attr({class: 'list-group-item list-group-item-danger'})
          .text("Error calling web service. Please try again later."));
      }
    });
  });

  $('#create-form-cancel-button').click(function(event){
    showHomeDisplay();
  });


  $("#search-button").click(function(event){
    clearDvdTable();
    $('#searchContentRows').empty();
    var haveValidationErrors = checkAndDisplayValidationErrors($("#search-form").find('input'));

    if (haveValidationErrors) {
      return false;
    }


    $.ajax({
      type: 'GET',
      url: 'http://localhost:52939/dvds/' + $('#search-category').val() + '/' + $('#search-term').val(),
      success: function(dvdArray) {
        $.each(dvdArray, function(index, dvd) {
          var title = dvd.Title;
          var releaseDate = dvd.ReleaseYear;
          var director = dvd.Director;
          var rating = dvd.Rating;
          var dvdId = dvd.DvdId;
          var row = '<tr>';
              row += '<td><a onclick="gotoTitle(' + DvdId + ')">' + Title + '</a></td>';
              row += '<td>' + releaseDate + '</td>';
              row += '<td>' + director + '</td>';
              row += '<td>' + rating + '</td>';
              row += '<td><a onclick="showEditForm(' + dvdId + ')">Edit</a> | <a id="deleteConfirm" onclick="deleteDvd(' + dvdId + ')">Delete</a></td>';
              row += '</tr>';

              clearDvdTable();
          $('#searchContentRows').append(row);
          $('#search-category').val('');
          $('#search-term').val('');
          $('#contentRows').hide();
          $('#searchContentRows').show();
          $('#search-back-button').show();

        });
      },
      error: function() {
        $("#errorMessages")
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text("Error calling web service. Please try again later."));
      }
    });
  });

    $('#search-back-button').click(function(event){
    $('#search-category').val('');
    $('#search-term').val('');
    $('#searchContentRows').hide();
    $('#search-back-button').hide();
    $('#contentRows').show();
    loadDvds();
  });

  $('#edit-button').click(function(event){

    var haveValidationErrors = checkAndDisplayValidationErrors($("#edit-form").find('input'));

    if (haveValidationErrors) {
      return false;
    }

    $.ajax({
      type: 'PUT',
      url: 'http://localhost:52939/dvd/' + $('#edit-dvd-id').val(),
      data: JSON.stringify({
        dvdId: $('#edit-dvd-id').val(),
        Title: $('#edit-title').val(),
        realeaseYear: $('#edit-release-year').val(),
        director: $('#edit-director').val(),
        rating: $('#edit-rating').val(),
        notes: $('#edit-notes').val()
      }),
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      'dataType': 'json',
      success: function() {
        $("#errorMessages").empty();
        hideEditForm();
        loadDvds();
      },
      error: function() {
        $('#errorMessages')
        .append($('<li>')
        .attr({class: 'list-group-item list-group-item-danger'})
        .text("Error calling web service. Please try again later."));
      }
    });
  });

  $('#edit-cancel-button').click(function(event){
    showHomeDisplay();
  });

});
// end of on load function

function loadDvds(){
  clearDvdTable();
  var contentRows = $("#contentRows");

  $.ajax({
    type: 'GET',
    url: 'http://localhost:52939/dvds/all',
    success: function(dvdArray) {
      $.each(dvdArray, function(index, dvd) {
        var title = dvd.Title;
        var releaseDate = dvd.ReleaseYear;
        var director = dvd.Director;
        var rating = dvd.Rating;
        var dvdId = dvd.DvdId;
        var row = '<tr>';
            row += '<td><a onclick="gotoTitle(' + dvdId + ')">' + title + '</a></td>';
            row += '<td>' + releaseDate + '</td>';
            row += '<td>' + director + '</td>';
            row += '<td>' + rating + '</td>';
            row += '<td><a onclick="showEditForm(' + dvdId + ')">Edit</a> | <a onclick="deleteDvd(' + dvdId + ')">Delete</a></td>';
            row += '</tr>';

        contentRows.append(row);
      });
    },
    error: function() {
      $("#errorMessages")
          .append($('<li>')
          .attr({class: 'list-group-item list-group-item-danger'})
          .text("Error calling web service. Please try again later."));
    }
  });
}


function showCreateDisplay(){
  $('#errorMessages').empty();
  $('#homeDisplayHeader').hide();
  $('#homeDisplayTable').hide();
  $('#editDisplayHeader').hide();
  $('#editDisplay').hide();
  $('#createDisplayHeader').show();
  $('#createDisplay').show();
}

function showHomeDisplay(){
  $('#errorMessages').empty();
  $('#homeDisplayHeader').show();
  $('#homeDisplayTable').show();
  $('#editDisplayHeader').hide();
  $('#editDisplay').hide();
  $('#createDisplayHeader').hide();
  $('#createDisplay').hide();
  $('#detailsDisplayHeader').hide();
  $('#detailDisplayTable').hide();
  $('#search-back-button').hide();
}

function showEditDisplay(){
  $('#errorMessages').empty();
  $('#homeDisplayHeader').hide();
  $('#homeDisplayTable').hide();
  $('#editDisplayHeader').show();
  $('#editDisplay').show();
  $('#createDisplayHeader').hide();
  $('#createDisplay').hide();
}

function gotoTitle(dvdId){
  clearDvdTable();
  $('#errorMessages').empty();
  var detailContentRows = $('#detailContentRows');

  $.ajax({
    type: 'GET',
    url: 'http://localhost:52939/dvd/' + dvdId,
    success: function(data, status) {

      $('#detailsH2').val(data.Title);

      var title = data.Title;
      $('#detailsH2').text(title);
      var dvdId = data.DvdId;
      var releaseDate = data.ReleaseYear;
      var director = data.Director;
      var rating = data.Rating;
      var notes = data.Notes;
      var table = '<tr>';
          table += '<td>Release Year:</td>';
          table += '<td>' + releaseDate + '</td>';
          table += '</tr><tr>';
          table += '<td>Director:</td>';
          table += '<td>' + director + '</td>';
          table += '</tr><tr>';
          table += '<td>Rating:</td>';
          table += '<td>' + rating + '</td>';
          table += '</tr><tr>';
          table += '<td>Notes:</td>';
          table += '<td>' + notes + '</td>';
          table += '</tr>';
          detailContentRows.append(table);
    },
    error: function() {
      $("#errorMessages")
          .append($('<li>')
          .attr({class: 'list-group-item list-group-item-danger'})
          .text("Error calling web service. Please try again later."));
    }
  });
  $('#homeDisplayHeader').hide();
  $('#homeDisplayTable').hide();
  $('#detailsDisplayHeader').show();
  $('#detailDisplayTable').show();
}

function showEditForm(dvdId){
  $('#errorMessages').empty();

  $.ajax({
    type: 'GET',
    url: 'http://localhost:52939/dvd/' + dvdId,
    success: function(data, status){
      var editH2 = "Edit Dvd: " + data.Title;

      $('#editH2').text(editH2);
      $('#edit-title').val(data.Title);
      $('#edit-release-year').val(data.ReleaseYear);
      $('#edit-director').val(data.Director);
      $('#edit-rating').val(data.Rating);
      $('#edit-notes').val(data.Notes);
      $('#edit-dvd-id').val(data.DvdId);
    },
    error: function() {
      $('#errorMessages')
        .append($('<li>'))
        .attr({class: 'list-group-item list-group-danger'})
        .text('Error calling web service. Please try again later.');
    }
  });

  showEditDisplay();
}

function hideEditForm()
{
  $('#errorMessages').empty();

  $('#edit-title').val('');
  $('#edit-release-year').val('');
  $('#edit-director').val('');
  $('#edit-rating').val('');
  $('#edit-notes').val('');
  showHomeDisplay();
}



function deleteDvd(dvdId){
  $.confirm({
      title: 'Confirmation',
      content: 'Are you sure you want to delete this Dvd from your collection?',
      buttons: {
        confirm: function () {
          $.alert('Title deleted!');
          $.ajax({
            type: 'DELETE',
            url: 'http://localhost:8080/dvd/' + dvdId,
            success: function(){
              loadDvds();
            }
          });
        },
        cancel: function() {
          $.alert('Canceled!');
        }
      }
  });
}


function clearDvdTable() {
  $("#contentRows").empty();
}

function checkAndDisplayValidationErrors(input) {
  $("#errorMessages").empty();

  var errorMessages = [];

  input.each(function() {
    if (!this.validity.valid) {
      var errorField = $('label[for=' + this.id +']'). text();
      var errorMessage = this.value;
      errorMessages.push(errorField + ' ' + errorMessage + ' is an invalid input.');
    }
  });

  if(errorMessages.length > 0 ){
    $.each(errorMessages, function(index, message){
      $('#errorMessages').append($('<li>').attr({class: 'list-group-item list-group-item-danger'}).text(message));
    });
    return true;
  } else {
    return false;
  }
}
