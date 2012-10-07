<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class posts extends CI_Controller 
{
	function __construct()
    {
        parent::__construct();
    }

	public function index()
	{	
		$this->check_credentials();
		redirect('posts/add');
		#$this->load->view('main_view', array( 'posts' => $this->posts->get()));
	}
    public function add()
    {
    	$this->check_credentials();
    	if($this->input->post('form_sent') == '1')
    	{
    		$this->posts_model->insert($this->input->post('title'),$this->input->post('body'),$this->input->post('date'));
    	}else{
	    	$this->load->view('forms/new_post_view');
    	}
    }

    private function check_credentials()
    {
    	if( $this->session->userdata('valid') != true)
    	{
    		redirect('posts/login');
    	}
    }
    
    public function login()
    {
    	if($this->input->post('form_sent') == 1)
    	{
    		if($this->input->post('username')==$this->config->item('aca_admin_username') &&  $this->input->post('password')==$this->config->item('aca_admin_pass'))
    		{
    			$this->session->set_userdata(array('valid'=>true));
    			redirect('posts/add');
    		}
    	}
    	$this->load->view('forms/posts_login_view');
    }
}
?>