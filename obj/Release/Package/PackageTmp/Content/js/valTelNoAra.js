$(document).ready(function () {
    $('#formTelNo').bootstrapValidator({
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            telefonno: {
                message: 'The username is not valid',
                validators: {
                    notEmpty: {
                        message: 'Telefon no alanı boş olamaz!'
                    },
                    stringLength: {
                        min: 10,
                        max: 10,
                        message: 'Telefon numarası 10 karakter uzunluğunda olmalıdır'
                    }
                }
            }
        }
    });
});