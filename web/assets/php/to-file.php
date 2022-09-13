<?php

if(isset($_POST['email'])) {
    define('EMAIL_FILE', 'emails.txt');

    $email = trim($_POST['email']);

    function valid_email($email)
    {
        return filter_var($email, FILTER_VALIDATE_EMAIL);
    }

    if (defined('EMAIL_FILE') && isset($email) && valid_email($email)) {
        file_put_contents(EMAIL_FILE, PHP_EOL . date("Y/m/d H:i:s") . " - " . "$email", FILE_APPEND);
    }
}

?>