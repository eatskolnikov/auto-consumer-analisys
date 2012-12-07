<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class posts_model extends CI_Model
{
	function __construct()
    {
        parent::__construct();
		$this->load->database();
    }
	function get()
	{
		$query = $this->db->query("SELECT * FROM posts");
		if ($query != false)
			return $query->result();
		return $query;
	}
	function insert($title, $body, $date)
	{
		$sql = "INSERT INTO posts(postid,title,body,date) VALUES(null,?,?,?)";
		$query = $this->db->query($sql,array($title,$body,$date));
	}
};
?>