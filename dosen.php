<?php
require_once "method.php";
$dosen = new Dosen();
$request_method = $_SERVER["REQUEST_METHOD"];

switch ($request_method) {
    case 'GET':
        if (!empty($_GET["id_dosen"])) {
            $id = intval($_GET["id_dosen"]);
            $dosen->get_dosen($id);
        } else {
            $dosen->get_all_dosen();
        }
        break;
    
    case 'POST':
        $dosen->insert_dosen();
        break;

    case 'PUT':
        parse_str(file_get_contents("php://input"), $_PUT);
        $id = intval($_GET["id_dosen"]);
        $dosen->update_dosen($id, $_PUT);
        break;

    case 'DELETE':
        $id = intval($_GET["id_dosen"]);
        $dosen->delete_dosen($id);
        break;
    
    default:
        header("HTTP/1.0 405 Method Not Allowed");
        break;
}
?>
