$(() => {
    let i = 1;
    $('#add-tags').click(function () {
        $('#form-input').append(`<input name="tags[${i}]" placeholder="Tag #${i + 1}" />`);
        i++;
    });
});