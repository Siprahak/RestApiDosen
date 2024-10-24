<!DOCTYPE html>
<html lang="en">
<head>
    <title>Add Data Dosen</title>
</head>
<body>
    <center>
        <h1>Tambah Data Dosen</h1>
        <form action="dosen.php" method="POST">
            <p>
                <label for="nama_dosen">Nama Dosen:</label>
                <input type="text" name="nama_dosen" id="nama_dosen" required>
            </p>
            <p>
                <label for="nidn">NIDN:</label>
                <input type="text" name="nidn" id="nidn" required>
            </p>
            <p>
                <label for="email">Email:</label>
                <input type="email" name="email" id="email" required>
            </p>
            <input type="submit" value="Submit">
        </form>
    </center>
</body>
</html>
