$(() => {
    $('#like-btn').click(function () {
        let questionID = $(this).data('id');
        $.post('/home/likeQuestion', { questionID }, function () {
            $("#like-btn").remove();
        });
    });

    $('#like-answer-btn').click(function () {
        let answerID = $(this).data('id');
        $.post('/home/likeAnswer', { answerID }, function () {
            $("#like-answer-btn").remove();
        });
    });

    $('#submit-answer').click(function () {
        let questionID = $(this).data('questionid');
        let userID = $(this).data('userid');
        let answerText = $('#answer-text').val();
        $.post('/home/addanswer', { questionID, userID, answerText }, function () {
            $('#answer-text').val("");
        });
    });
});