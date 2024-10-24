<?php
// Mengatur variabel koneksi database
$host = "localhost";
$user = "root";
$pass = "";
$db = "dbdosen";

// Membuat koneksi menggunakan objek mysqli
$mysqli = new mysqli($host, $user, $pass, $db);

// Cek koneksi
if ($mysqli->connect_error) {
    die("Connection failed: " . $mysqli->connect_error);
}
?>
