<?php
$servername = "boostworks.online";
$username = "boostworksonline_ced-app";
$password = "SyoYiTgKYj$6925&aF3y";
$db = "boostworksonline_ced";

// Create connection
$conn = new mysqli($servername, $username, $password, $db);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

// Variables from user app (Unity)
$studentId = $_POST['userID']; // Fill with unique student ID
$feelingScore = $_POST['feelingScore']; // Fill with feeling (1 to 5)
$feelingComment = $_POST['feelingComment']; // Fill with optional comment
$timestamp = date('Y-m-d'); // Current day (year, month, day)

$sql = "INSERT INTO feelings (student_id, score, comment, created_at) VALUES ('$studentId', '$feelingScore', '$feelingComment', '$timestamp')";

$resultId = $conn->query($sql);

$conn->close();

?>