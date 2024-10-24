<?php
require_once "method.php";
$mengajar = new Mengajar();
$request_method = $_SERVER["REQUEST_METHOD"];

switch ($request_method) {
    case 'GET':
        if (!empty($_GET["id_mengajar"])) {
            $id = intval($_GET["id_mengajar"]);
            $mengajar->get_mengajar($id);
        } else {
            $mengajar->get_all_mengajar();
        }
        break;
    
    case 'POST':
        $mengajar->insert_mengajar();
        break;

    case 'PUT':
        parse_str(file_get_contents("php://input"), $_PUT);
        $id = intval($_GET["id_mengajar"]);
        $mengajar->update_mengajar($id, $_PUT);
        break;

    case 'DELETE':
        $id = intval($_GET["id_mengajar"]);
        $mengajar->delete_mengajar($id);
        break;
    
    default:
        header("HTTP/1.0 405 Method Not Allowed");
        break;
}
?>
