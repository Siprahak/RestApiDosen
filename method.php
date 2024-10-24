<?php
require_once "koneksi.php";

class Dosen {
    public function get_all_dosen() {
        global $mysqli;
        $query = "SELECT * FROM dosen";
        $result = $mysqli->query($query);
        $data = array();
        while ($row = mysqli_fetch_object($result)) {
            $data[] = $row;
        }
        header('Content-Type: application/json');
        echo json_encode($data);
    }

    public function get_dosen($id) {
        global $mysqli;
        $query = "SELECT * FROM dosen WHERE id_dosen = $id LIMIT 1";
        $result = $mysqli->query($query);
        $data = array();
        if ($row = mysqli_fetch_object($result)) {
            $data = $row;
        }
        header('Content-Type: application/json');
        echo json_encode($data);
    }

    public function insert_dosen() {
        global $mysqli;
        $nama = $_POST["nama_dosen"];
        $nidn = $_POST["nidn"];
        $email = $_POST["email"];
        $query = "INSERT INTO dosen (nama_dosen, nidn, email) VALUES ('$nama', '$nidn', '$email')";
        $result = $mysqli->query($query);
        $response = $result ? array("status" => 1, "message" => "Data Added Successfully.") : array("status" => 0, "message" => "Failed to Add Data.");
        header('Content-Type: application/json');
        echo json_encode($response);
    }

    public function update_dosen($id, $data) {
        global $mysqli;
        $nama = $data["nama_dosen"];
        $nidn = $data["nidn"];
        $email = $data["email"];
        $query = "UPDATE dosen SET nama_dosen = '$nama', nidn = '$nidn', email = '$email' WHERE id_dosen = $id";
        $result = $mysqli->query($query);
        $response = $result ? array("status" => 1, "message" => "Data Updated Successfully.") : array("status" => 0, "message" => "Failed to Update Data.");
        header('Content-Type: application/json');
        echo json_encode($response);
    }

    public function delete_dosen($id) {
        global $mysqli;
        $query = "DELETE FROM dosen WHERE id_dosen = $id";
        $result = $mysqli->query($query);
        $response = $result ? array("status" => 1, "message" => "Data Deleted Successfully.") : array("status" => 0, "message" => "Failed to Delete Data.");
        header('Content-Type: application/json');
        echo json_encode($response);
    }
}

class Matakuliah {
    public function get_all_matakuliah() {
        global $mysqli;
        $query = "SELECT * FROM matakuliah";
        $result = $mysqli->query($query);
        $data = array();
        while ($row = mysqli_fetch_object($result)) {
            $data[] = $row;
        }
        header('Content-Type: application/json');
        echo json_encode($data);
    }

    public function get_matakuliah($id) {
        global $mysqli;
        $query = "SELECT * FROM matakuliah WHERE id_matakuliah = $id LIMIT 1";
        $result = $mysqli->query($query);
        $data = array();
        if ($row = mysqli_fetch_object($result)) {
            $data = $row;
        }
        header('Content-Type: application/json');
        echo json_encode($data);
    }

    public function insert_matakuliah() {
        global $mysqli;
        $nama = $_POST["nama_matakuliah"];
        $kode = $_POST["kode_matakuliah"];
        $sks = intval($_POST["sks"]);
        $query = "INSERT INTO matakuliah (nama_matakuliah, kode_matakuliah, sks) VALUES ('$nama', '$kode', $sks)";
        $result = $mysqli->query($query);
        $response = $result ? array("status" => 1, "message" => "Data Added Successfully.") : array("status" => 0, "message" => "Failed to Add Data.");
        header('Content-Type: application/json');
        echo json_encode($response);
    }

    public function update_matakuliah($id, $data) {
        global $mysqli;
        $nama = $data["nama_matakuliah"];
        $kode = $data["kode_matakuliah"];
        $sks = intval($data["sks"]);
        $query = "UPDATE matakuliah SET nama_matakuliah = '$nama', kode_matakuliah = '$kode', sks = $sks WHERE id_matakuliah = $id";
        $result = $mysqli->query($query);
        $response = $result ? array("status" => 1, "message" => "Data Updated Successfully.") : array("status" => 0, "message" => "Failed to Update Data.");
        header('Content-Type: application/json');
        echo json_encode($response);
    }

    public function delete_matakuliah($id) {
        global $mysqli;
        $query = "DELETE FROM matakuliah WHERE id_matakuliah = $id";
        $result = $mysqli->query($query);
        $response = $result ? array("status" => 1, "message" => "Data Deleted Successfully.") : array("status" => 0, "message" => "Failed to Delete Data.");
        header('Content-Type: application/json');
        echo json_encode($response);
    }
}

class Mengajar {
    public function get_all_mengajar() {
        global $mysqli;
        $query = "SELECT * FROM mengajar";
        $result = $mysqli->query($query);
        $data = array();
        while ($row = mysqli_fetch_object($result)) {
            $data[] = $row;
        }
        header('Content-Type: application/json');
        echo json_encode($data);
    }

    public function get_mengajar($id) {
        global $mysqli;
        $query = "SELECT * FROM mengajar WHERE id_mengajar = $id LIMIT 1";
        $result = $mysqli->query($query);
        $data = array();
        if ($row = mysqli_fetch_object($result)) {
            $data = $row;
        }
        header('Content-Type: application/json');
        echo json_encode($data);
    }

    public function insert_mengajar() {
        global $mysqli;
        $id_dosen = intval($_POST["id_dosen"]);
        $id_matakuliah = intval($_POST["id_matakuliah"]);
        $semester = $_POST["semester"];
        $tahun = $_POST["tahun_ajaran"];
        $query = "INSERT INTO mengajar (id_dosen, id_matakuliah, semester, tahun_ajaran) VALUES ($id_dosen, $id_matakuliah, '$semester', '$tahun')";
        $result = $mysqli->query($query);
        $response = $result ? array("status" => 1, "message" => "Data Added Successfully.") : array("status" => 0, "message" => "Failed to Add Data.");
        header('Content-Type: application/json');
        echo json_encode($response);
    }

    public function update_mengajar($id, $data) {
        global $mysqli;
        $id_dosen = intval($data["id_dosen"]);
        $id_matakuliah = intval($data["id_matakuliah"]);
        $semester = $data["semester"];
        $tahun = $data["tahun_ajaran"];
        $query = "UPDATE mengajar SET id_dosen = $id_dosen, id_matakuliah = $id_matakuliah, semester = '$semester', tahun_ajaran = '$tahun' WHERE id_mengajar = $id";
        $result = $mysqli->query($query);
        $response = $result ? array("status" => 1, "message" => "Data Updated Successfully.") : array("status" => 0, "message" => "Failed to Update Data.");
        header('Content-Type: application/json');
        echo json_encode($response);
    }

    public function delete_mengajar($id) {
        global $mysqli;
        $query = "DELETE FROM mengajar WHERE id_mengajar = $id";
        $result = $mysqli->query($query);
        $response = $result ? array("status" => 1, "message" => "Data Deleted Successfully.") : array("status" => 0, "message" => "Failed to Delete Data.");
        header('Content-Type: application/json');
        echo json_encode($response);
    }
}
?>
