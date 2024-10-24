<html>
<head>
    <style>
        table {
            font-family: arial, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }
        td, th {
            border: 1px solid #dddddd;
            padding: 8px;
            text-align: center;
        }
        tr:nth-child(even) {
            background-color: #dddddd;
        }
    </style>
</head>
<body>
    <h1>Data Dosen</h1>
    <a href="tambahData.php" target="_blank">Tambah Data Dosen</a>
    <table>
        <tr>
            <th>No.</th>
            <th>ID Dosen</th>
            <th>Nama Dosen</th>
            <th>NIDN</th>
            <th>Email</th>
        </tr>
        <?php
        $link = "http://localhost/dbdosen/dosen.php";
        $json_list = file_get_contents($link);
        $array = json_decode($json_list, true);
        $result = isset($array['data']) ? $array['data'] : array();
        $no = 1;
        foreach ($result as $record) {
            echo "<tr>
                <td>$no</td>
                <td>{$record['id_dosen']}</td>
                <td>{$record['nama_dosen']}</td>
                <td>{$record['nidn']}</td>
                <td>{$record['email']}</td>
            </tr>";
            $no++;
        }
        ?>
    </table>
</body>
</html>
