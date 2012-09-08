<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class posts extends CI_Model
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
}
?>