<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class site extends CI_Controller 
{
	function __construct()
    {
        parent::__construct();
    }

	public function index()
	{
		$this->load->view('main_view', array( 'posts' => $this->posts_model->get()));
	}
}
?>