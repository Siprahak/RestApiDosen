<?php
require_once "method.php";
$matakuliah = new Matakuliah();
$request_method = $_SERVER["REQUEST_METHOD"];

switch ($request_method) {
    case 'GET':
        if (!empty($_GET["id_matakuliah"])) {
            $id = intval($_GET["id_matakuliah"]);
            $matakuliah->get_matakuliah($id);
        } else {
            $matakuliah->get_all_matakuliah();
        }
        break;
    
    case 'POST':
        $matakuliah->insert_matakuliah();
        break;

    case 'PUT':
        parse_str(file_get_contents("php://input"), $_PUT);
        $id = intval($_GET["id_matakuliah"]);
        $matakuliah->update_matakuliah($id, $_PUT);
        break;

    case 'DELETE':
        $id = intval($_GET["id_matakuliah"]);
        $matakuliah->delete_matakuliah($id);
        break;
    
    default:
        header("HTTP/1.0 405 Method Not Allowed");
        break;
}
?>
